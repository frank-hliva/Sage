module Sage.Parsing.Checks

let isValidExpressionList = function
| [] -> true
| nonEmpty ->
    match nonEmpty |> List.last with
    | ast.Let _ -> false
    | _ -> true