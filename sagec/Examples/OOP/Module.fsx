#light "off"
#nowarn "62";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\StdLib.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.Object.Prototyped.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.dll";;
open StdLib;;
open StdLib.Object;;
open Sage.Core.Object.Prototyped;;
open Sage.Core;;
let (=>) a b = a, box b in
let emit (str : string) = begin () end in
let say (value : 't) = begin printf "%s" (value.ToString()) end in
let sayn (value : 't) = begin printfn "%s" (value.ToString()) end in
(fun my currentModuleName -> begin
(* BEGIN MODULE *)
let my = my |> Module.set "Test" ((fun my currentModuleName -> begin
(* BEGIN MODULE *)
let rec tst = fun () -> begin
sayn "TEST"
end in
let my = my |> Module.set "tst" tst in
my
(* END MODULE *)
end) ("Test" |> Module.empty) "Test") in
((my.["Test"] :?> table).["tst"] :?> unit -> unit)();
sayn ("Hello");
my
(* END MODULE *)
end) ("App" |> Module.empty) "App";

