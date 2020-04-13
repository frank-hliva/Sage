module Sage.Output.ML.CodeGen.Modules

open StdLib.Text
open Sage
open Sage.Parsing
open Sage.Output.ML.CodeGen
open System

(*

zakazat assignovanie:
    - my
    - currentModuleName
    - vsetky symboly
    - vsetky operatory
    - vsetky keywordy
    - porozmyslat na vyescapovani nazvov
*)

let blockToList = function
| Block block -> block
| _ -> failwith "Invalid module"

let joinBlockExprList (generateAst : ast -> string) (exprs : ast list) =
    exprs
    |> List.map
        (fun expr ->
            (match expr with
            | Let _ -> sprintf "%s in"
            | _ -> sprintf "%s") (expr |> generateAst)
            |> fun result ->
                if result.EndsWith(" in")
                then result else sprintf "%s;" result
        )
    |> fun exprs -> exprs |> String.join "\n"

let wrapToModuleFunction moduleName (moduleType : moduleType) (body : string) =
    [
        sprintf "%s(fun my currentModuleName -> begin"
            (match moduleType with
            | moduleType.Root -> ""
            | moduleType.Assign -> "("
            | _ -> sprintf "let my = my |> Module.set %s (" moduleName)
        "(* BEGIN MODULE *)"
        sprintf "%s" body
        "my"
        "(* END MODULE *)"
        sprintf "end) (%s |> Module.empty) %s%s"
            moduleName moduleName
            (match moduleType with
            | moduleType.Root -> ";"
            | moduleType.Assign -> ")"
            | _ -> ") in")
    ]
    |> String.join "\n"

let genModule (ctx : generatorContext) (name : ast, moduleType : moduleType, body : ast) =
    let { genKey = genModuleName } = ctx;
    body
    |> blockToList
    |> joinBlockExprList ctx.generateAst
    |> wrapToModuleFunction (name |> genModuleName) moduleType