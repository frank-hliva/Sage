module Sage.LexicalAnalysis.Lexer

open System
open System.Globalization
open StdLib.Text
open Sage
open Sage.LexicalAnalysis
open Sage.LexicalAnalysis.Keywords
open Sage.LexicalAnalysis.Symbols
open Sage.LexicalAnalysis.Punc
open Sage.LexicalAnalysis.Operators
open Sage.LexicalAnalysis.Special
open Sage.LexicalAnalysis.Rules
open Sage.Parsing
open Sage.Parsing.ParserImpl

module internal Operators =
    let (+=) = Position.addColumn
    let (/=) = Position.newLine
    let (!) = List.rev >> Chars.toString

module internal Errors =
    let invalidEscapeSequence = LanguageError.toError "Invalid escape sequence"
    let invalidSymbol = LanguageError.toError "Invalid symbol"
    let invalidString = LanguageError.toError "Invalid string"
    let invalidNumber = LanguageError.toError "Invalid number"

module internal WhiteSpace =
    let toLength = function
    | '\t' -> 4 | _ -> 1

open Operators

let tokenize (input : string) : lexBuff =
    let rec comment (pos : position) (acc : chars) = function
    | '\n' :: t -> pos /= 1, acc, t
    | [] -> pos, acc, []
    | c :: t -> t |> comment (pos += 1) (c :: acc)
    let rec multilineComment (pos : position) (acc : chars) = function
    | ')' :: '#' :: t -> pos += 2, acc, t
    | [] -> pos, acc, []
    | ('\n' as c) :: t -> t |> multilineComment (pos /= 1) (c :: acc)
    | c :: t -> t |> multilineComment (pos += 1) (c :: acc)
    and number (pos : position) (acc : chars) = function
    | IsDigit c :: t -> t |> number (pos += 1) (c :: acc)
    | t -> pos, acc, t
    and eols (pos : position) = function
    | '\n' :: t -> t |> eols (pos /= 1)
    | IsWhiteSpace c :: t -> t |> eols (pos += 1)
    | t -> pos, t
    and whiteSpaces (pos : position) (acc : chars) (size : int) = function
    | IsWhiteSpace c :: t -> t |> whiteSpaces (pos += 1) (c :: acc) (size + (c |> WhiteSpace.toLength))
    | t -> pos, acc, t, size
    and identifier (pos : position) (acc : chars) = function
    | IsIdentifier c :: t -> t |> identifier (pos += 1) (c :: acc)
    | t -> pos, acc, t
    and escape (pos : position) (acc : chars) = function
    | IsCharIn String.Escape.chars c :: t ->
        t |> multilineString (pos += 1) (String.Escape.map.[c] :: acc)
    | 'u' :: t ->
        match t |> String.Escape.tryParseUnicodeEscape with
        | Some (char, t) ->
            t |> multilineString (pos += 5) (char :: acc)
        | _ -> Errors.invalidEscapeSequence pos |> failwith
    | _ -> Errors.invalidEscapeSequence pos |> failwith
    and multilineString (pos : position) (acc : chars) = function
    | '\\' :: t -> t |> escape (pos += 1) acc
    | '"' :: t -> pos += 1, acc, t
    | ('\n' as c) :: t -> t |> multilineString (pos /= 1) (c :: acc)
    | c :: t -> t |> multilineString (pos += 1) (c :: acc)
    | [] -> Errors.invalidString pos |> failwith
    and tokenize (pos : position) (tokens : lexBuff) = function
    | '#' :: '(' :: t ->
        let posa, comment, t = t |> multilineComment (pos += 2) []
        t |> tokenize posa ((pos, MULTILINE_COMMENT(!comment)) :: tokens)
    | '#' :: t ->
        let posa, comment, t = t |> comment (pos += 1) []
        t |> tokenize posa ((pos, SINGLELINE_COMMENT(!comment)) :: (pos, SEPARATOR) :: tokens)
    | '(' :: t ->
        t |> tokenize (pos += 1) ((pos, LPAREN) :: tokens)
    | ')' :: t ->
        t |> tokenize (pos += 1) ((pos, RPAREN) :: tokens)
    | '[' :: t ->
        t |> tokenize (pos += 1) ((pos, LBRACKET) :: tokens)
    | ']' :: t ->
        t |> tokenize (pos += 1) ((pos, RBRACKET) :: tokens)
    | '{' :: t ->
        t |> tokenize (pos += 1) ((pos, LBRACE) :: tokens)
    | '}' :: t ->
        t |> tokenize (pos += 1) ((pos, RBRACE) :: tokens)
    | IsIdentifierStart c :: t ->
        let posa, identifier, t = t |> identifier (pos += 1) [c]
        let token =
            match !identifier with
            | IsKeyword (keyword, token) -> token
            | IsSymbol symbol -> SYMBOL(symbol)
            | identifier -> IDENTIFIER(identifier)
        let tokens = 
            match token with
            | KW_ELIF -> ((pos, KW_IF) :: (pos, KW_ELSE) :: tokens)
            | KW_ELIF_NOT -> ((pos, KW_IF_NOT) :: (pos, KW_ELSE) :: tokens)
            | _ -> ((pos, token) :: tokens)
        t |> tokenize posa tokens
    | ParseSpecial((special, token), t) ->
        t |> tokenize (pos += special.Length) ((pos, token) :: tokens)
    | '"' :: t ->
        let posa, str, t = t |> multilineString (pos += 1) []
        t |> tokenize posa ((pos, Token.STRING(!str)) :: tokens)
    | IsDigitStart c :: t ->
        let posa, number, t = t |> number (pos += 1) [c]
        match !number |> Convert.tryToDecimal with
        | Some decimal -> t |> tokenize posa ((pos, NUMBER(decimal)) :: tokens)
        | _ -> Errors.invalidNumber pos |> failwith
    | IsWhiteSpace c :: t ->
        let posa, whiteSpaces, t, len = t |> whiteSpaces (pos += 1) [c] (c |> WhiteSpace.toLength)
        let token =
            match pos.Column with
            | 0 -> INDENTATION(!whiteSpaces, len)
            | _ -> WHITESPACES(!whiteSpaces, len)
        t |> tokenize posa ((pos, token) :: tokens)
    | ('\n' :: _) as t ->
        let pos, t = t |> eols pos
        t |> tokenize pos ((pos, SEPARATOR) :: tokens)
    | ';' :: t -> t |> tokenize pos ((pos, SEPARATOR) :: tokens)
    | _ :: t ->
        Errors.invalidSymbol pos |> failwith
    | [] -> (pos, EOF) :: tokens
    input
    |> fun content -> content.Replace("\r\n", "\n")
    |> Chars.ofString
    |> tokenize (Position.empty) []
    |> TokenCollector.collect None []