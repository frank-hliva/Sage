module internal Sage.Parsing.Errors

open Sage

let invalidNumber n = LanguageError.toError (sprintf "Invalid number: \"%s\"" n)
let invalidSymbol symbol = LanguageError.toError (sprintf "Invalid symbol: \"%s\"" symbol)
let unsupportedKeyword keyword = LanguageError.toError (sprintf "Unsupported keyword: \"%s\"" keyword)
let syntaxError = LanguageError.toError (sprintf "Syntax error")
let parseError = LanguageError.toError (sprintf "Parse error")
let expectedExpression = LanguageError.toError (sprintf "Expected expression")
let invalidUsingOfLetKeyword = LanguageError.toError (sprintf "Invalid usage of the let keyword")
let invalidOperator = LanguageError.toError (sprintf "Invalid operator usage")