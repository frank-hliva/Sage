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
let rec add5 = add 5 in
let my = my |> Module.set "add5" add5 in
let rec mul4 = fun a -> begin
let rec x = a * 4 in
let my = my |> Module.set "x" x in
x * 10000
end in
let my = my |> Module.set "mul4" mul4 in
(if x = 5 then 5 else 10);
let rec minus = fun a b -> begin
a - b
end in
let my = my |> Module.set "minus" minus in
let rec minus4 = minus 4 in
let my = my |> Module.set "minus4" minus4 in
alert ((add 4) 5);
let rec x = null in
let my = my |> Module.set "x" x in
let rec y = () in
let my = my |> Module.set "y" y in
print "Fero Hliva 
";
5 |> mul4 |> add 10.5 |> minus4 |> alert;
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"