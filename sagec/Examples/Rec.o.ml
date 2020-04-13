#light "off"
#nowarn "62";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\StdLib.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.Object.Prototyped.dll";;
#r "c:\\MyLang\\Sage\\Sage\\bin\\Debug\\netstandard2.0\\Sage.Core.dll";;
let (=>) a b = a, box b in
let emit (str : string) = begin () end in
let say (value : 't) = begin printf "%s" (value.ToString()) end in
let sayn (value : 't) = begin printfn "%s" (value.ToString()) end in
let my = my |> Module.set "Main" (fun my currentModuleName -> begin
(* BEGIN MODULE *)
let rec loop = fun x max -> begin
((if x = max then () else begin
alert x;
let rec y = 1 in
let my = my |> Module.set "y" y in
loop (x + y) max
end))
end in
let my = my |> Module.set "loop" loop in
loop 0 5;
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"