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
let rec x = fun foo x -> begin
foo x
end in
let my = my |> Module.set "x" x in
let rec a = 2 in
let my = my |> Module.set "a" a in
(fun () -> begin
let rec y = "Paula berglová" in
let my = my |> Module.set "y" y in
alert ("Ahoj: " + y)
end) ();
let rec m = (fun x -> begin
alert ("x: " + x)
end) in
let my = my |> Module.set "m" m in
x ((if a <> 1 then alert else (fun x -> begin
alert ("x: " + x)
end))) "Hello world";
let rec add = (fun a b -> begin
a + b
end) 5 in
let my = my |> Module.set "add" add in
alert (add 5);
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"