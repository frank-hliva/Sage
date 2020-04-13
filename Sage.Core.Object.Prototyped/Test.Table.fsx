#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"

#load "Core.Object.fs"
#load "Core.Object.Table.fs"

open StdLib
open Sage.Core.Object

let table = Table.empty()
printfn "1. %b" ((table |> Table.set "name" "Fero Hliva") |> Table.equals (dict ["name", box "Fero Hliva"]))
printfn "2. %b" ((table |> Table.set "age" "37") |> Table.equals (dict ["name", box "Fero Hliva"; "age", box "37"]))
printfn "3. %b" ((table |> Table.containsKey "age"))
printfn "4. %b" ((table |> Table.contains "Fero Hliva"))
printfn "5. %b" ((table |> Table.filter(fun x v -> x <> "name") |> Table.equals (dict ["age", box "37"])))
printfn "6. %b" ((table |> Table.map(fun x v -> v.ToString() + v.ToString() |> box)) |> Table.equals (dict ["name", box "Fero HlivaFero Hliva"; "age", box "3737"]))
printfn "7. %b" ((table |> Table.get "name") = box "Fero Hliva")
printfn "8. %b" ((table |> Table.tryGet "name") = Some(box "Fero Hliva"))
let table1 = table |> Table.shallowCopy
printfn "9. %b" (table1 |> Table.equals table)
table1 |> Table.set "name" ((table1 |> Table.get "name" :?> string) + " & copy")
printfn "10. %b" (table1 |> Table.equals table |> not)