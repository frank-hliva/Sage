#light "off"
#nowarn "62";;
let (=>) a b = a, box b in
let emit (str : string) = begin () end in
let print (value : 't) = begin emit "console.log(value);\n" end in
let alert (value : 't) = begin printfn "%s" (value.ToString()) end in

(match x with 
| x -> 5
| _ -> 10);
let rec x = 5 in
fero |> Instance.sendMessage (KeywordMessage(["equals" ==> sandra]))
# PIPE
fero |> Instance.sendMessage (KeywordMessage(["ifTrue" ==> "Equals";"ifFalse" ==> "Not Equals"]))
# PIPE
fero |> Instance.sendMessage (UnaryMessage("alert"));
fero |> Instance.sendMessage (KeywordMessage(["equals" ==> sandra;"ifTrue" ==> ("Equals");"ifFalse" ==> ("Not Equals")]))
# PIPE
fero |> Instance.sendMessage (UnaryMessage("alert"))