#light "off"
#nowarn "62";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\StdLib.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.Object.Prototyped.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.dll";;
open StdLib;;
open Sage.Core.Object.Prototyped;;
open Sage.Core;;
let (=>) a b = a, box b in
let emit (str : string) = begin () end in
let say (value : 't) = begin printf "%s" (value.ToString()) end in
let sayn (value : 't) = begin printfn "%s" (value.ToString()) end in
(fun my currentModuleName -> begin
(* BEGIN MODULE *)
let my = my |> Module.set "Math" ((fun my currentModuleName -> begin
(* BEGIN MODULE *)
let rec plus = fun a b -> begin
a + b
end in
let my = my |> Module.set "plus" plus in
let rec minus = fun a b -> begin
a - b
end in
let my = my |> Module.set "minus" minus in
let my = my |> Module.set "Test" ((fun my currentModuleName -> begin
(* BEGIN MODULE *)
let my = my |> Module.set "Test1" ((fun my currentModuleName -> begin
(* BEGIN MODULE *)
let rec plus5 = fun a -> begin
plus a 5
end in
let my = my |> Module.set "plus5" plus5 in
let rec plus10 = plus 10 in
let my = my |> Module.set "plus10" plus10 in
let rec print = fun () -> begin
say (plus 1 1)
end in
let my = my |> Module.set "print" print in
let rec print20 = fun () -> begin
say (plus10 10)
end in
let my = my |> Module.set "print20" print20 in
my
(* END MODULE *)
end) ("Test1" |> Module.empty) "Test1") in
my
(* END MODULE *)
end) ("Test" |> Module.empty) "Test") in
my
(* END MODULE *)
end) ("Math" |> Module.empty) "Math") in
Math.Test.Test1.print ()
say (Math.Test.Test1.plus5 10)
say (Math.Test.Test1.plus10 2)
say (Math.Test.Test1.print20 ())
my
(* END MODULE *)
end) ("App" |> Module.empty) "App";