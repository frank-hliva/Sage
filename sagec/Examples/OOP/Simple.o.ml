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
let rec fero = (Person 
|> Instance.sendMessage(KeywordMessage(["withName" ==> "Frank Hliva"; "andAge" ==> 37]))) in
let my = my |> Module.set "fero" fero in
let rec sandra = (Person 
|> Instance.sendMessage(KeywordMessage(["withName" ==> "Sandra Belkova"; "andAge" ==> 29]))) in
let my = my |> Module.set "sandra" sandra in
fero 
|> Instance.sendMessage(KeywordMessage(["equals" ==> sandra; "ifTrue" ==> ("Equals"); "ifFalse" ==> ("Not Equals")]))
fero 
|> Instance.sendMessage(UnaryMessage("alert"));
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"