module Sage.Core.Object.Prototyped.Meta

open System
open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message

type metaData = keyValueSeq

module Keys =
    let [<Literal>] meta = "!meta"
    let [<Literal>] id = "!id"
    let [<Literal>] elementType = "!elementType"
    let [<Literal>] name = "!name"

let private metaProp (metaData : metaData) =
    metaData
    |> Table.ofSeq
    |> Table.set Keys.id (Guid.NewGuid() |> box)

let create (metaData : metaData) (target : table) =
    target |> Table.set Keys.meta (metaProp metaData)

let get (metaProp : string) (table : table) =
    table
    |> Table.get Keys.meta
    :?> table
    |> Table.get metaProp