module Sage.LexicalAnalysis.Keywords

open StdLib
open Sage
open Parsing.ParserImpl

let keywords = [
    "let" => KW_LET
    "if" => KW_IF
    "ifnot" => KW_IF_NOT
    "then" => KW_THEN
    "else" => KW_ELSE
    "elif" => KW_ELIF
    "elifnot" => KW_ELIF_NOT
    "func" => KW_FUNC
    "match" => KW_MATCH

    "kind" => KW_KIND
    "protocol" => KW_PROTOCOL
    "self" => KW_SELF
    "module" => KW_MODULE
    "package" => KW_PACKAGE

    "alloc" => KW_ALLOC
    "stackalloc" => KW_STACKALLOC
]

let private keywordMap = Map keywords

let (|IsKeyword|_|) (input : string) =
    match keywordMap |> Map.tryFind(input) with
    | Some token -> Some (input, token)
    | _ -> None