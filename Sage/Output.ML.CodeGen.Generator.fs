module Sage.Output.ML.CodeGen.Generator

open System
open System.Globalization
open StdLib.Text
open Sage
open Sage.Parsing
open Sage.Output.ML.CodeGen.Messaging
open Sage.Output.ML.CodeGen.Modules

let rec generate (ast : ast) = 
    """include <stdio.h>

""" + (ast |> generateAst)

and generateAst = function
| Number n -> sprintf "%s" (n.ToString(CultureInfo.InvariantCulture))
| Str str -> sprintf "\"%s\"" str
| Bool b -> sprintf "%b" b
| Nil -> "null"
| Unit -> "()"
| Identifier ident -> sprintf "%s" ident
| IdentifiersPath identifiers -> identifiers |> String.join "."
| Parens expr -> expr |> generateAst |> sprintf "(%s)" 
| ExprList exprs -> exprs |> wrapExpr genExprList
| Let (left, right) -> left |> genAssign (right |> generateAst)
| Function (isCurried, args, body) -> sprintf "%s %s" (args |> genArgs isCurried) (body |> genFunctionBody args)
| FunctionEval (isCurried, identifierOrPath, args) -> genFunctionEval isCurried identifierOrPath args
| SendMessages (receiver, messages) -> (receiver, messages) |> Sender.genSendMessages genratorContext
| ExprEval (expr, evalArgs) -> sprintf "(%s) %s" (expr |> generateAst) (evalArgs |> genEvalArgs)
| If (cond, then', else') -> sprintf "(if %s then %s else %s)" (cond |> generateAst) (then' |> generateAst) (else' |> genElse)
| IfNot (cond, then', else') -> sprintf "if (!(%s)) then %s else %s)" (cond |> generateAst) (then' |> generateAst) (else' |> genElse)
| BinaryOperator (operator, left, right) -> (operator |> toBinaryOperator) (left |> generateAst) (right |> generateAst)
| UnaryOperator (operator, right) -> (operator |> toUnaryOperator) (right |> generateAst)
| List items -> items |> genList
| Record items -> items |> genRecord
| Module (name, isRoot, body) -> (name, isRoot, body) |> genModule genratorContext
| Match (expr, matchingList) -> expr |> genMatch matchingList
| Root _ | Block _ -> failwith "Invalid element"

and genAssign (value : string) (name : string) =
    sprintf "let rec %s = %s in\nlet my = my |> Module.set \"%s\" %s"
        name value name name

and genFunctionEval isCurried identifierOrPath args =
    sprintf "%s %s"
        (identifierOrPath |> generateAst)
        (args |> (if isCurried then genEvalArgs else genNonCurriedEvalArgs))

and isStatement = function
| Let _ -> true
| _ -> false 

and wrapExpr (genExprList : bool -> ast list -> string) = function
| [] -> "()"
| expr :: [] when expr |> isStatement |> not ->
    expr |> generateAst |> sprintf "(%s)" 
| exprs ->
    exprs |> genExprList true |> sprintf "%s"

and genArgs isCurried = function
| [] -> "fun () ->"
| args when isCurried -> args |> List.map(sprintf "%s") |> String.join " " |> sprintf "fun %s ->"
| args -> args |> String.join ", " |> sprintf "fun (%s) ->"

and genEvalArgs =
    List.map(generateAst >> sprintf "%s") >> String.join " "

and genNonCurriedEvalArgs =
    List.map(generateAst) >> String.join ", " >> sprintf "(%s)"

and joinExprList (withReturn : bool) (exprs : ast list) =
    let last = exprs.Length - 1
    exprs
    |> List.mapi
        (fun i expr ->
            (if i = last
            then 
                if withReturn then sprintf "%s"
                else sprintf "%s"
            else
                match expr with
                | Let _ -> sprintf "%s in"
                | _ -> sprintf "%s;") (expr |> generateAst))
    |> fun exprs -> exprs |> String.join "\n"

and genExprList withReturn = joinExprList withReturn >> sprintf "begin\n%s\nend"

and genMatch (matchingList : matching list) (expr : ast) =
    sprintf "(match %s with %s)"
        (expr |> generateAst)
        (matchingList |> genMatchingList)

and genMatchingList (matchingList : matching list) =
    matchingList
    |> List.map
        (fun (left, right) ->
            match left with
            | case.Expr left -> left |> generateAst |> sprintf "| %s"
            | case.CaseIdentifier left -> left |> sprintf "| %s"
            | _ -> "| _"
            |> fun left ->
                match right with

                | Some right -> sprintf "%s -> %s" left (right |> generateAst)
                | _ -> left
        )
    |> String.join "\n" |> sprintf "\n%s"

and genList = List.map (generateAst >> sprintf "box (%s)") >> String.join "; " >> sprintf "[%s]"

and genKey = function
| Identifier ident
| Str ident -> sprintf "\"%s\"" ident
| expr -> sprintf "(%s)" (expr |> generateAst)

and genRecord (items : (ast * ast) list) =
    items
    |> List.map
        (fun (key, value) ->
            sprintf "%s => %s" (key |> genKey) (value |> generateAst))
    |> String.join "; "
    |> sprintf "dict [%s]"

#if ARGS_ENABLED

and toArgsLetBinding args = (Let("args", List(args |> List.map(Identifier))))

and genFunctionBody args = function
| ExprList exprs -> ((toArgsLetBinding args) :: exprs) |> genExprList true
| expr -> [ (toArgsLetBinding args); expr ] |> genExprList true

#else

and genFunctionBody args = function
| ExprList exprs -> exprs |> genExprList true
| expr -> [ expr ] |> genExprList true

#endif

and genElse = function
| Some else' -> else' |> generateAst
| _ -> "()"

and toBinaryOperator = function
| "bw_xor" -> sprintf "%s ^ %s"
| "bw_or" -> sprintf "%s | %s"
| "bw_and" -> sprintf "%s & %s"
| "bw_not" -> sprintf "%s ~ %s"
| "<<:" -> sprintf "%s << %s"
| ":>>" -> sprintf "%s >> %s"
| "xor" -> failwith "undefined logical XOR" //sprintf "(true = (!!%s <> !!%s)))"
| "or" -> sprintf "%s || %s"
| "and" -> sprintf "%s && %s"
| "<>" -> sprintf "%s <> %s"
| "=" -> sprintf "%s == %s"
| "%%" -> failwith "undefined operator"
| "**" -> sprintf "Math.Pow(%s, %s)"
| "|>" -> sprintf "%s |> %s"
| "<|" -> sprintf "%s <| %s"
| ">>" -> sprintf "%s >> %s"
| "<<" -> sprintf "%s << %s"
| operator -> (fun a b -> String.Format("{0} {1} {2}", a, operator, b))

and toUnaryOperator = function
| "succ" -> sprintf "(%s + 1)"
| "pred" -> sprintf "(%s - 1)"
| operator -> (fun a -> String.Format("({0}{1})", operator, a))

and genratorContext =
    {
        generateAst = generateAst
        genKey = genKey
        genAssign = genAssign
        joinExprList = joinExprList
    }