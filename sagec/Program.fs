module rxx.App

open System
open System.IO
open System.Reflection
open StdLib
open StdLib.IO
open Sage.Parsing

let compile =
    File.ReadAllText >> Parser.parse >> ignore
    
let rec compileAll = function
| [] -> printfn "\nSuccess"
| path :: t ->
    printf "compiling: %s" path
    path |> compile
    printfn " ... OK"
    t |> compileAll

let sourcesPath =
    AppDomain.CurrentDomain.BaseDirectory |> Path.parent 2

let relativePathToFull (path : string) =
    [sourcesPath; path] |> Path.join

[<EntryPoint>]
let main argv =
#if DEBUG
    let argv = [| @"/Examples/first.sage" |> relativePathToFull |]
#endif
    match argv |> List.ofArray with
    | [] -> printfn "%s" Resources.exeInfo
    | paths -> paths |> compileAll
    Console.ReadLine() |> ignore
    0