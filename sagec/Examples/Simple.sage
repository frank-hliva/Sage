let x = "name"

let toFieldValue = func field ->
    match field:
    | "name" -> "Fero Hliva"
    | "number"
    | "age" -> "37"
    | _ -> "Nothing"

alert (toFieldValue "name")
alert (toFieldValue "age")
alert (toFieldValue "number")

let toFieldValue2 = func field ->
    match field:
    | "name" -> "Fero Hliva"
    | "number"
    | "age" -> "37"
    | x -> x