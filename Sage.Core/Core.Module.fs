module Sage.Core.Module

open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped

module Keys =
    let [<Literal>] prototype = "!prototype"
    let [<Literal>] proto = "!proto"
    let [<Literal>] constructor = "!constructor"
    let [<Literal>] access = "!access"

let [<Literal>] elementType = "module"

module Meta =

    let createBy (name : string) (creator : Meta.metaData -> Meta.metaData) (target : table) =
        target
        |> Meta.create ([
            Meta.Keys.name => name
            Meta.Keys.elementType => elementType
            Keys.access => Table.empty()
        ] |> creator)

    let create (name : string) (target : table) =
        createBy name id target

    let createWith (name : string) (metadata : Meta.metaData) (target : table) =
        createBy name (Seq.append metadata) target

let ofSeq (name : string) (seq : keyValueSeq) =
    seq |> Table.ofSeq |> Meta.create name

let empty (name : string) = ofSeq name []

let get (key : string) (target : table) =
    target |> Table.get key

let set (key : string) (value : 't) (target : table) =
    target |> Table.set key (box value)