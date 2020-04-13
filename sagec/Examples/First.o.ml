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
let rec foo = fun a b c -> begin
let rec x = 50 in
let my = my |> Module.set "x" x in
let rec x = (if a = 1 && b <> 2 then 10 else 5) in
let my = my |> Module.set "x" x in
x
end in
let my = my |> Module.set "foo" foo in
let rec x = (foo 1 2 3) + (-5) in
let my = my |> Module.set "x" x in
();
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"