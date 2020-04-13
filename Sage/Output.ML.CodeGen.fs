namespace Sage.Output.ML.CodeGen

open System
open StdLib.Text
open Sage
open Sage.Parsing
open System.Globalization

type generatorContext =
    {
        generateAst : ast -> string
        genKey : ast -> string
        genAssign : string -> string -> string
        joinExprList : bool -> ast list -> string
    }

module NameGenerator =

    let private toPrefix = function
    | null | "" -> "tmp"
    | prefix -> prefix

    let genPrefTmpName (prefix : string) =
        sprintf "%s%s"
            (prefix |> toPrefix)
            (Guid.NewGuid().ToString().Replace("-", ""))

    let genTmpName () =
        genPrefTmpName null