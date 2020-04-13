module Sage.LexicalAnalysis.Special

open Sage.LexicalAnalysis.Elements
open Sage.LexicalAnalysis.Operators
open Sage.LexicalAnalysis.Punc

let specials = operators @ puncs

let spac1 = specials |> toElementStartSet
let spac2 = specials |> toElementList

let (|ParseSpecial|_|) =
    tryParseElement
        (specials |> toElementStartSet)
        (specials |> toElementList)