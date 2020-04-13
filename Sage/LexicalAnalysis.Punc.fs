module Sage.LexicalAnalysis.Punc

open StdLib
open Sage
open Sage.LexicalAnalysis.Elements
open Parsing.ParserImpl

let puncs = [
    "," => COMMA
    ":" => COLON
    ";" => SEPARATOR
    "|" => BAR
    "_" => ANYTHING
    "λ" => LAMBDA
    "^" => NON_CURRIED
    "|" => BAR
]

let puncSet = puncs |> Set.ofList