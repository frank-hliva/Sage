namespace Sage.Core.Object

open System
open System.Collections.Generic
open System.Linq

type key = string
type value = obj
type table = IDictionary<key, value>

module Table =

    let empty() = new Dictionary<key, value>() :> table

    let shallowCopy (dict : table) = new Dictionary<key, value>(dict)

    let ofSeq (sequence : (key * value) seq) =
        sequence.ToDictionary(fst, snd) :> table

    let ofKeyValuepairsSeq (sequence : KeyValuePair<key, value> seq) =
        sequence.ToDictionary((fun kv -> kv.Key), (fun kv -> kv.Value)) :> table

    let equals (tab2 : table) (tab1 : table) =
        tab1.Keys.Count = tab2.Keys.Count &&
        tab1.Keys.All(fun k -> tab2.ContainsKey(k) && obj.Equals(tab2.[k], tab1.[k]))

    let set key value (tab : table) =
        tab.[key] <- value
        tab

    let add = set

    let get key (tab : table) =
        tab.[key]

    let tryGet key (tab : table) =
        let exists, value = tab.TryGetValue(key)
        if exists then Some value
        else None

    let remove (key : key) (tab : table) =
        tab.Remove(key) |> ignore
        tab

    let containsKey (key : key) (tab : table) =
        tab.ContainsKey(key)

    let find (predicate : key -> value -> bool) (tab : table) =
        tab.First(fun kv -> predicate kv.Key kv.Value).Value

    let tryFind (predicate : key -> value -> bool) (tab : table) =
        try
            Some (tab.First(fun kv -> predicate kv.Key kv.Value).Value)
        with
        | :? Exception -> None

    let contains (value : obj) (tab : table) =
        tab.Any(fun x -> x.Value = value)

    let filter (predicate : key -> value -> bool) (tab : table) =
        tab
        |> Seq.filter(fun kv -> predicate kv.Key kv.Value)
        |> ofKeyValuepairsSeq

    let map (mapper : key -> value -> value) (tab : table) =
        tab
        |> Seq.map
            (fun kv ->
                kv.Key, (mapper kv.Key kv.Value))
        |> ofSeq

    let iter (iterator : key -> value -> unit) (tab : table) =
        tab
        |> Seq.iter
            (fun kv ->
                iterator kv.Key kv.Value)