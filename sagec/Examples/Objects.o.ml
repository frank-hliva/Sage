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
let rec age = 37 in
let my = my |> Module.set "age" age in
let rec foo = fun () -> begin
"key"
end in
let my = my |> Module.set "foo" foo in
let rec object = dict ["name" => "Fero Hliva"; "age" => age; (foo ()) => 5] in
let my = my |> Module.set "object" object in
let rec x = [box (2); box ("Fero"); box (6)] in
let my = my |> Module.set "x" x in
let rec obj = dict ["name" => "Sandra Belková"; "age" => 30] in
let my = my |> Module.set "obj" obj in
();
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"