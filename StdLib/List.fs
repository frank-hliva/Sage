module List

let rec private mapRevLoop (acc : 'b list) (mapper : 'a -> 'b) = function
| [] -> acc
| h :: t -> t |> mapRevLoop ((mapper h) :: acc) mapper

let mapRev<'a, 'b> = mapRevLoop<'b, 'a> (List.empty<'b>)

let rec private mapRevPrevLoop (acc : 'b list) (before : 'a option) (mapper : 'a option -> 'a -> 'b) = function
| [] -> acc
| h :: t ->
    t |> mapRevPrevLoop ((mapper before h) :: acc) (Some h) mapper

let mapRevPrev<'a, 'b> = mapRevPrevLoop<'b, 'a> (List.empty<'b>) None

let rec private mapPrevLoop k (before : 'a option) (mapper : 'a option -> 'a -> 'b) = function
| [] -> k []
| h :: t ->
    let mapped = h |> mapper before
    t |> mapPrevLoop (fun xs -> k (mapped :: xs)) (Some h) mapper

let mapPrev<'a, 'b> = mapPrevLoop<'b, 'a0, 'a> id None