module Sage.Output.ML.CodeGen.Messaging.Sender

open System
open System.Globalization
open StdLib
open StdLib.Text
open Sage
open Sage.Parsing
open Sage.Output.ML.CodeGen

let genSendMessages (ctx : generatorContext) (receiver, messages : messageWrapper list) =
    let { generateAst = generateAst; genKey = genKey } = ctx;

    let rec genMessages (messages : messageWrapper list) (receiver : ast) =
        messages
        |> List.mapPrev (genMessageWrapper receiver)
        |> String.join ""

    and genMessageWrapper (receiver : ast) (prev : messageWrapper option) = function
    | messageWrapper.Msg msg -> msg |> genMessage receiver (prev |> wasPipe |> not)
    | messageWrapper.Join -> "\n"
    | messageWrapper.Pipe -> "\n|>"

    and wasPipe = function
    | Some wrapper ->
        match wrapper with
        | messageWrapper.Pipe -> true
        | _ -> false
    | _ -> false

    and genInstanceSendMessage (withReceiver : bool) (receiver : ast) =
        if withReceiver then sprintf "%s \n|>" (receiver |> generateAst) else ""
        |> sprintf "%s Instance.sendMessage"

    and genMessage (receiver : ast) (withReceiver : bool) (msg : msg)  = 
        printfn "withReceiver = %b" withReceiver
        (receiver |> genInstanceSendMessage withReceiver) +
            (match msg with
            | msg.UnaryMessage key -> key |> genUnaryMessage receiver
            | msg.BinaryMessage msg -> msg |> genBinaryMessage receiver
            | msg.KeywordMessage keywords -> keywords |> genKeywordMessage receiver)

    and genUnaryMessage (receiver : ast) (msg : ast) =
        sprintf "(UnaryMessage(%s))"
            (msg |> genKey)

    and genBinaryMessage (receiver : ast) (binaryMsg : binaryMessage) =
        sprintf "(BinaryMessage(%s))"
            (match binaryMsg with
            | binaryMessage.Value (key, value) -> sprintf "(%s ==> %s)" (key |> genKey) (value |> generateAst)
            | binaryMessage.Undefined key -> sprintf "(!!%s)" (key |> genKey))

    and genKeywordPart kwPart = kwPart |> Seq.map genKeyword |> String.join "; " |> sprintf "[%s]"

    and genKeywordMessage (receiver : ast) (keywords : Parsing.keyword list) =
        sprintf "(KeywordMessage(%s))"
            (keywords |> genKeywordPart)

    and genKeyword = function
    | keyword.Value (key, value) -> sprintf "%s ==> %s" (key |> genKey) (value |> generateAst)
    | keyword.Undefined key -> sprintf "!!%s" (key |> genKey)
    | keyword.Placeholder key -> sprintf "!%s" (key |> genKey)
    | _ -> failwith "Message undivided"

    receiver |> genMessages messages