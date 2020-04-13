module Sage.Parsing.Parser

open Sage
open Sage.Parsing
open FSharp.Text.Lexing
open FSharp.Text.Parsing
open Sage.LexicalAnalysis
open Sage.Parsing.ParserInfo
open StdLib
open System

#if !NOYACC

type private Position with
    member pos.Update(pos_bol : int, pos_lnum : int, pos_cnum : int) = 
        let pos = pos
        { pos with
            pos_bol = pos_bol
            pos_lnum = pos_lnum
            pos_cnum = pos_cnum }

type TokenReader(buff : lexBuff, fsLexBuff: LexBuffer<char>, positionStore : PositionStore, tokenStore : TokenStore) =
    let counter = new Counter()

    member r.Read() =
        let p, token = buff.[counter.Fetch() |> int]
        positionStore.SetPosition(p)
        fsLexBuff.StartPos <- fsLexBuff.StartPos.Update(p.Position, p.Line, p.Column)
        fsLexBuff.EndPos <- fsLexBuff.StartPos.Update(p.Position, p.Line, p.Column)
        match token with
        | Token.INDENTATION _
        | Token.WHITESPACES _
        | Token.SINGLELINE_COMMENT _ 
        | Token.MULTILINE_COMMENT _ ->            
            counter.Increment() |> ignore
            r.Read()
        | _ ->
            match tokenStore.GetCurrentToken() with
            | Some tokenBefore when (tokenBefore |> Token'.isSeparator) && (token |> Token'.isSeparator) ->
                counter.Increment() |> ignore
                r.Read()
            | Some tokenBefore when (tokenBefore |> Token'.isOperator) && (token |> Token'.isSeparator) ->
                counter.Increment() |> ignore
                r.Read()
            | _ ->
                tokenStore.SetToken token
#if DEBUG
                printfn "%d %s" (counter.Fetch()) (token.ToString())
#endif
                counter.Increment() |> ignore
                match token with
                | Token.EOF -> counter.Stop()
                | _ -> ()
                token

    interface IDisposable with
        member c.Dispose() =
            (counter :> IDisposable).Dispose()

let private lexer (reader : TokenReader) (buff : LexBuffer<char>): ParserImpl.token =
    reader.Read()

type ParserResult =
| Success of ast
| ParseError of message : string * position : position

let tryParse (input : string) =
    let fsLexBuff = LexBuffer<char>.FromString input
    use positionStore = new PositionStore()
    use tokenStore = new TokenStore()
    use reader = new TokenReader(input |> Lexer.tokenize, fsLexBuff, positionStore, tokenStore)
    try
        (lexer reader |> ParserImpl.start) fsLexBuff |> Success
    with
    | :? Exception as e ->
        ParseError(
            e.Message,
            positionStore.GetPosition()
        )

let (|Module|_|) = function
| Module (key, isRoot, body) -> Some (key, isRoot, body)
| _ -> None

let private wrapIfNeeded = function
| Root exprs ->
    match exprs with
    | [] -> Identifier "Main", Block []
    | Module(key, _, body) :: [] -> key, body
    | exprs -> Identifier "Main", Block exprs
    |> fun (key, root) -> Module(key, moduleType.Root, root)
| _ -> failwith "Parse error"

let parse (input : string) =
    match input |> tryParse with
    | Success ast -> ast |> wrapIfNeeded
    | ParseError(message, position) ->
        position |> (message |> LanguageError.toError) |> failwith

#endif