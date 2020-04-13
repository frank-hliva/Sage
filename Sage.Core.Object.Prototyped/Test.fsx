open System.Text

#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"

#load "Core.Object.fs"
#load "Core.Object.Table.fs"
#load "Core.Object.Prototyped.fs"
#load "Core.Object.Prototyped.Message.fs"
#load "Core.Object.Prototyped.Member.fs"
#load "Core.Object.Prototyped.Object.fs"

open StdLib
open Sage.Core.Object
open Sage.Core.Object.Table
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Member
open Sage.Core.Object.Prototyped.Message


let (=>) a b = a, b // kedze F# nema ziadny pekny operator na slovniky nadefinoval som operator dvojitu sipku nech je ten kod zrozumitelny

let dictionary = 
    [
      "aaa" => "123"
      "b" => "45"
      "c" => "67"
      "def" => "89"
      "gh" => "11"
    ]
    |> dict
    |> System.Collections.Generic.Dictionary<_, _>


// mame mnozinu dvoch klucov ktore chceme hladat
let keys = ["gh"; "c"] |> Set.ofList

module Dict =

    //kedze sme v F# a C#kove kolekcie nepodporuju typ option nadefinoval som pomocnu funkciu tryGet 
    // to je ale vedlajsie ide nam teraz o funkciu TryGetValue

    let tryGet key (tab : table) = 
        let exists, value = tab.TryGetValue(key)
        if exists then Some value
        else None

