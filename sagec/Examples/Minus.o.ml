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
let rec add = fun a b -> begin
a + b
end in
let my = my |> Module.set "add" add in
let rec foo1 = fun x y -> begin
(-y)
end in
let my = my |> Module.set "foo1" foo1 in
let rec diff = fun a b -> begin
a - b
end in
let my = my |> Module.set "diff" diff in
let rec c2 = (diff 10 5) in
let my = my |> Module.set "c2" c2 in
();
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"