module Sage.LexicalAnalysis.String.Escape

open System
open System.Globalization
open StdLib.Text

let private replacements =
    [
        '\\', '\\'
        '"', '"'
        ''', '''
        'a', '\a'
        'b', '\b'
        'f', '\f'
        'n', '\n'
        'r', '\r'
        't', '\t'
        'v', '\v'
    ]

let chars = replacements |> List.map(fst)
let map = Map replacements

let tryParseUnicodeEscape = function
| Chars.ParseN 4 (chars, t) ->
    try
        Some(chars |> Chars.hexToBin |> char, t)
    with
    | :? Exception as e -> None
| _ -> None