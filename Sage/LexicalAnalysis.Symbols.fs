module Sage.LexicalAnalysis.Symbols

let symbols = [
    "true"
    "false"
    "nil"
    "self"
    "super"
    "null" // compatibility with json
]

let private symbolSet = symbols |> Set.ofList

let (|IsSymbol|_|) (input : string) =
    if input |> symbolSet.Contains
    then Some input
    else None