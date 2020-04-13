#r "netstandard"
#r @"c:/Users/hliva/.nuget/packages/fslexyacc.runtime/9.0.2/lib/netstandard2.0\FsLexYacc.Runtime.dll"
#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"
#r @"c:/MyLang/Sage/Sage.Core.Object.Prototyped/bin/Debug/netstandard2.0/Sage.Core.Object.Prototyped.dll"

#load "Position.fs"
#load "Parsing.fs"
#load "Parsing.Checks.fs"
#load "Parsing.Convert.fs"
#load "Parsing.ParserImpl.fs"
#load "LexicalAnalysis.fs"
#load "LexicalAnalysis.Rules.fs"
#load "LexicalAnalysis.Elements.fs"
#load "LexicalAnalysis.Keywords.fs"
#load "LexicalAnalysis.Symbols.fs"
#load "LexicalAnalysis.Punc.fs"
#load "LexicalAnalysis.Operators.fs"
#load "LexicalAnalysis.Special.fs"
#load "LexicalAnalysis.Token.fs"
#load "LexicalAnalysis.String.Escape.fs"
#load "LexicalAnalysis.TokenCollector.fs"
#load "LexicalAnalysis.Lexer.fs"
#load "Parsing.Errors.fs"
#load "Parsing.PositionStore.fs"
#load "Parsing.TokenStore.fs"
#load "Parsing.Parser.fs"
#load "Output.fs"
#load "Output.ML.CodeGen.fs"
#load "Output.ML.CodeGen.Messaging.Sender.fs"
#load "Output.ML.CodeGen.Modules.fs"
#load "Output.ML.CodeGen.Generator.fs"

open System
open System.IO
open StdLib.IO
open Sage.LexicalAnalysis
open Sage.Parsing
open Sage.Output.ML.CodeGen

let build (sourcePath : string) =
    sourcePath
    |> File.ReadAllText
    |> Parser.parse
    |> Generator.generate
    |> fun out -> File.WriteAllText(Path.ChangeExtension(sourcePath, "o.ml"), out) 

let parse (sourcePath : string) =
    sourcePath
    |> File.ReadAllText
    |> Parser.parse

let tokenize (sourcePath : string) =
    sourcePath
    |> File.ReadAllText
    |> Lexer.tokenize

let buildAll (sourcePath : string) =
    Console.ResetColor()
    Directory.EnumerateFiles(sourcePath, "*.sage")
    |> Seq.iter
        (fun path ->
            try
                path |> build
                Console.ForegroundColor <- ConsoleColor.Green
                printfn "Success: %s" path
            with
            | :? Exception ->
                Console.ForegroundColor <- ConsoleColor.Red
                printfn "Failed: %s" path)
    Console.ResetColor()

let root = __SOURCE_DIRECTORY__ |> Path.parent 1
let sourcePath = Path.join([root;"sagec/Examples/OOP/SimpleModule.sage"])
sourcePath |> tokenize
sourcePath |> parse
sourcePath |> build

@"c:\MyLang\Sage\sagec\Examples\" |> buildAll
@"c:\MyLang\Sage\sagec\Examples\OOP\" |> buildAll

