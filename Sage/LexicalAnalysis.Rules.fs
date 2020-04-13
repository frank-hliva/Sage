module Sage.LexicalAnalysis.Rules

open System
open StdLib.Text
open Sage

let rec (|IsLetter|_|) = function
| c when c |> Char.IsLetter -> Some c
| _ -> None

and (|IsIdentifierStart|_|) = function
| '_' | '$' as c
| IsLetter c -> Some c
| _ -> None

and (|IsIdentifier|_|) = function
| IsIdentifierStart c
| IsInteger c -> Some c
| ''' -> Some '''
| _ -> None

and (|IsDigitStart|_|) = function
| IsInteger c -> Some c
| _ -> None

and (|IsDigit|_|) = function
| IsInteger c -> Some c
| '.' as c -> Some c
| _ -> None

and (|IsInteger|_|) = function
| c when c |> Char.IsDigit -> Some c
| _ -> None

and (|IsWhiteSpace|_|) = function
| ' ' | '\t' as c -> Some c
| _ -> None

and (|IsCharIn|_|) (chars : chars) (char : char) =
    if chars |> List.contains char
    then Some char
    else None