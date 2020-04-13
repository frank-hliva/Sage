module Sage.LexicalAnalysis.TokenCollector

open Sage
open Sage.LexicalAnalysis
open Sage.Parsing.ParserImpl

let private wasBar = function
| Some token when token |> Token'.isBar -> true
| _ -> false

let private wasSpace = function
| Some token when token |> Token'.isSpace -> true
| _ -> false

let private wasBreakLineOperator = function
| Some token when token |> Token'.isLineBreakOperators -> true
| _ -> false

let rec collect (previousToken : token option) (acc : lexBuff) = function
| [] -> acc
| x :: xs ->
    let _, token = x
    match token with
    | Token.SEPARATOR when previousToken |> wasBreakLineOperator ->
        xs |> collect previousToken acc
    | Token.OP_SUBTRACT when previousToken |> wasSpace |> not ->
        xs |> collect (Some token) ((x |> fst, OP_NEGATE) :: acc)
    | Token.OP_POWER when previousToken |> wasSpace |> not ->
        xs |> collect (Some token) ((x |> fst, OP_DOUBLE_STAR) :: acc)
    | Token.OP_MULTIPLY when previousToken |> wasSpace |> not ->
        xs |> collect (Some token) ((x |> fst, OP_STAR) :: acc)
    | _ ->
        xs |> collect (Some token) (x :: acc)