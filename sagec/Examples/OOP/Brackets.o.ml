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
let rec aaa = Person 
|> Instance.sendMessage(KeywordMessage([!"init"; "withName" ==> "Fero Hliva"; "andAge" ==> 37])) in
let my = my |> Module.set "aaa" aaa in
let rec bbb = Person 
|> Instance.sendMessage(KeywordMessage([!"init"; "withName" ==> "Fero Hliva"; "andAge" ==> null])) in
let my = my |> Module.set "bbb" bbb in
let my = my |> Module.set "Object" (fun my currentModuleName -> begin
(* BEGIN MODULE *)
let rec addMethod = external in
let my = my |> Module.set "addMethod" addMethod in
my;
(* END MODULE *)
end) ("Object" |> Module.empty) "Object" in;
let rec person = Person 
|> Instance.sendMessage(UnaryMessage("init")) in
let my = my |> Module.set "person" person in
let rec ccc = Person 
|> Instance.sendMessage(KeywordMessage([!"init"; "withName" ==> "Fero Hliva"; "andAge" ==> (fero |> getAge); "piped" ==> xxx]))
|> Instance.sendMessage(KeywordMessage(["and" ==> xxx]))
Person 
|> Instance.sendMessage(KeywordMessage(["piped1" ==> xxx; "and1" ==> xxxhhh])) in
let my = my |> Module.set "ccc" ccc in
();
my;
(* END MODULE *)
end) ("Main" |> Module.empty) "Main"