module Sage.Core.Object.Prototyped.Object

open System
open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message

module Keys =
    let [<Literal>] prototype = "!prototype"
    let [<Literal>] proto = "!proto"
    let [<Literal>] constructor = "!constructor"
    let [<Literal>] access = "!access"

let [<Literal>] elementType = "object"

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

let private prototypeProp (name : string) =
    Table.empty() |> Meta.create name

let internal empty (name : string) = 
    [
        Keys.prototype => ((prototypeProp name) |> box)
    ] |> Table.ofSeq

let internal ofSeq (name : string) (seq : keyValueSeq) =
    seq |> Table.ofSeq |> Table.set Keys.prototype (prototypeProp name)

let internal tryGetKey<'t> (key : string) (tab : table) =
    match tab |> Table.tryFind(fun k _ -> k = key) with
    | Some value -> value :?> 't |> Some
    | _ -> None

let internal tryFindPrototype (tab : table) =
    tab |> tryGetKey<table> Keys.prototype

let internal getPrototype (tab : table) =
    defaultArg (tab |> tryFindPrototype) (Table.empty())

let private toKeywordArgs
    (keyNameMap : Map<key, key>)
    (optKeywordMap : Map<key, value messageOption>)
    (optKeyword : optKeywordSeq) =
    optKeyword
    |> Seq.choose
        (fun (k, optV) ->
            match optV with
            | Just value -> (keyNameMap.[k], value) |> Some
            | Undefined -> (keyNameMap.[k], optKeywordMap.[k] |> Opt.toValue) |> Some
            | Placeholder -> None
        )
    |> Map

let private addMethodToTable (msg : message) (m : method) (tab : table) =
    let propStamp = msg |> Message.toMessageStamp
    tab |> Table.set propStamp m

let internal addMethod (target : Target) (msg : message) (m : method) (tab : table) =
    match target with
    | Target.Prototype ->
        tab.[Keys.prototype]
        :?> table
        |> addMethodToTable msg m
        |> fun prototype ->
            tab |> Table.set Keys.prototype prototype
    | _ -> tab |> addMethodToTable msg m

let addMethodForUnaryMessage (target : Target) (key : key) (func : objectContext -> 't) (tab : table) =
    tab
    |> addMethod
        target
        (UnaryMessage key)
        (fun msg ctx ->
            match msg with
            | UnaryMessage key -> ctx |> func |> box
            | _ -> failwith "Invalid message, except: unary message"
        )

let addMethodForBinaryMessage (target : Target) (key : key, optDefaultValue : value messageOption) (func : keywordMap -> objectContext -> 't) (tab : table) =
    let key, name = key |> Key.toKeyName
    tab
    |> addMethod
        target
        (BinaryMessage (key, Undefined))
        (fun msg ctx ->
            match msg with
            | BinaryMessage (_, optValue) ->
                let value = 
                    match optValue with
                    | Just value -> value
                    | _ -> optDefaultValue |> Opt.toValue
                ctx |> func (Map[name, value]) |> box
            | _ -> failwith "Invalid message, except: binary message"
        )

let addMethodForKeywordMessage (target : Target) (keywords : keywordOrder) (func : keywordMap -> objectContext -> 't) (tab : table) =
    let keywordSeq = keywords |> KeywordOrder.toSeq
    let aliases = keywordSeq |> Seq.map(fst >> Key.toKeyName) |> Map
    let optKeyword = keywordSeq |> Seq.map(fun (k, v) -> (k |> Key.toKeyName |> fst), v)
    let optKeywordMap = optKeyword |> Map
    tab
    |> addMethod
        target
        (optKeyword |> (keywords |> KeywordOrder.toWrapper) |> KeywordMessage)
        (fun msg ctx ->
            match msg with
            | KeywordMessage keywordType ->
                let args = keywordType |> KeywordOrder.toSeq |> toKeywordArgs aliases optKeywordMap
                ctx |> func args |> box
            | _ -> failwith "Invalid message, except: keyword message"
        )

let internal toInstance (tab : table) =
    tab
    |> Table.shallowCopy
    |> Table.remove Keys.prototype
    |> Table.set Keys.proto (tab |> getPrototype)

let extends (name : string) (parent : table) (child : table) =
    let parentPrototype = parent |> getPrototype
    let f = empty name |> Table.set Keys.prototype parentPrototype
    child
    |> Table.set Keys.prototype (f |> toInstance |> Table.set Keys.constructor child |> Meta.create name)

module Named =

    let empty = empty

    let ofSeq = ofSeq

    let extends = extends

module Anonymous =

    let empty () = empty null

    let ofSeq : keyValueSeq -> _ = ofSeq null

    let extends : table -> table -> table = extends null