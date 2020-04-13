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
let rec x = "name" in
let my = my |> Module.set "x" x in
let rec toFieldValue = fun field -> begin
(match field with 
| "name" -> "Fero Hliva"
| "number"
| "age" -> "37"
| _ -> "Nothing")
end in
let my = my |> Module.set "toFieldValue" toFieldValue in
alert (toFieldValue "name");
alert (toFieldValue "age");
alert (toFieldValue "number");
let rec toFieldValue2 = fun field -> begin
(match field with 
| "name" -> "Fero Hliva"
| "number"
| "age" -> "37"
| x -> x)
end in
let my = my |> Module.set "toFieldValue2" toFieldValue2 in
();
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"