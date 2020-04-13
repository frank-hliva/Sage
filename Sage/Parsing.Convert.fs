[<RequireQualifiedAccess>]
module Sage.Parsing.Convert

open System
open System.Globalization
open Sage
open Sage.Parsing
open Sage.Parsing.Checks

let private tryParseNumber (parse: string -> bool * 't) (input : string) =
    let isValid, value = parse(input)
    if isValid then Some value else None

let tryToByte = tryParseNumber Byte.TryParse
let tryToInt = tryParseNumber Int32.TryParse
let tryToInt64 = tryParseNumber Int64.TryParse
let tryToSingle = tryParseNumber (fun n -> Single.TryParse(n, NumberStyles.Float, CultureInfo.InvariantCulture))
let tryToDouble = tryParseNumber (fun n -> Double.TryParse(n, NumberStyles.Float, CultureInfo.InvariantCulture))
let tryToDecimal = tryParseNumber (fun n -> Decimal.TryParse(n, NumberStyles.Float, CultureInfo.InvariantCulture))

let toSymbol = function
| "true" -> Bool true
| "false" -> Bool false
| "nil" -> Nil
| "null" -> Nil
| symbol -> failwithf "Invalid symbol %s" symbol

let toExprList exprList =
    if exprList |> isValidExpressionList
    then exprList |> ExprList
    else failwith "Invalid expression"

let toRootExprList exprList =
    if exprList |> isValidExpressionList
    then exprList
    else exprList @ [ast.Unit]
    |> Root