module internal Sage.LexicalAnalysis.Elements

open System
open StdLib.Text
open Sage
open Sage.Parsing.ParserImpl
open Sage.LexicalAnalysis.Rules

let toElementStartSet = List.map(fun (x : string, token : Token) -> x.[0]) >> Set.ofList

let toElementList = List.sortByDescending(fun (x : string, token : Token) -> x.Length)

let private isNotAlphaNum (element : string) =
    match element.[element.Length - 1] with
    | IsIdentifier _ -> false | _ -> true

let private isIsolated = function
| IsIdentifier _ :: _ -> false | _ -> true

let private elementChooser (input : chars) (element : string, token : Token) = 
    if input.Length < element.Length then None
    else
        if (input |> List.take element.Length |> String.Concat) = element
        then
            let t = input |> List.skip element.Length
            if isNotAlphaNum element || isIsolated t then
                Some((element, token), t)
            else None
        else None

let tryParseElement (elementStartSet : char Set) (elements : (string * Token) list) (input : chars) =
    if input.Length > 0 && elementStartSet.Contains(input.[0])
    then
        let chooser = elementChooser input
        elements
        |> Seq.filter (fun (element, _) -> input.[0] = element.[0])
        |> Seq.tryPick chooser
    else None