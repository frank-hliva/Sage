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
let rec addNonCurried = fun (a, b) -> begin
a + b
end in
let my = my |> Module.set "addNonCurried" addNonCurried in
alert (add 3 7);
alert (addNonCurried (6, 6));
let rec add2 = add 2 in
let my = my |> Module.set "add2" add2 in
let rec add4 = add2 >> add2 in
let my = my |> Module.set "add4" add4 in
alert (add4 5);
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"