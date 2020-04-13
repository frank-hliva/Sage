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
let rec minusOne = fun a -> begin
let rec b = 5 in
let my = my |> Module.set "b" b in
a - 2 - b
end in
let my = my |> Module.set "minusOne" minusOne in
let rec add5minus1 = add 5 >> minusOne in
let my = my |> Module.set "add5minus1" add5minus1 in
alert (4 + 5 |> add5minus1 |> add 4);
alert <| begin
let rec x = 5 in
let my = my |> Module.set "x" x in
x * x
end;
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"