module Sage.Core.Object.Prototyped.Instance

open System
open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message
open Sage.Core.Object.Prototyped.Member

let tryFindProto (tab : table) =
    tab |> Object.tryGetKey<table> Object.Keys.proto

let getProto (tab : table) =
    defaultArg (tab |> tryFindProto) (Table.empty())

let rec internal tryFindMemberInProto (memberQuery : Member.memberQuery) (memberStamps : Query.memberStampMap) (tab : table) =
    match tab |> tryFindProto with
    | Some proto ->
        match proto |> Member.tryFindByStamps memberQuery memberStamps with
        | Some memberInfo -> Some memberInfo
        | _ -> proto |> tryFindMemberInProto memberQuery memberStamps
    | _ -> None

and internal tryFindMember (memberQuery : Member.memberQuery) (tab : table) =
    let memberStamps = memberQuery |> Member.Query.toMemberStamps
    match tab |> Member.tryFindByStamps memberQuery memberStamps with
    | Some m -> Some m
    | _ -> tab |> tryFindMemberInProto memberQuery memberStamps

and internal context_sendMessage (messageQuery : messageQuery) (context : objectContext) =
    match context.self |> tryFindMember (messageQuery |> Member.memberQuery.Message) with
    | Some mi -> context |> (mi.member' :?> method) mi.message.Value
    | _ -> context |> messageNotReceived messageQuery

and internal super_sendMessage (messageQuery : messageQuery) (context : objectContext) =
    match context.super |> tryFindMember (messageQuery |> Member.Query.ofMessage) with
    | Some mi ->
        {
            context with
                super = context.super |> getProto
                sendMode = messageSendMode.Super
        } |> (mi.member' :?> method) mi.message.Value
    | _ -> context |> messageNotReceived messageQuery

and sendMessage (messageQuery : messageQuery) (tab : table) =
    {
        self = tab
        super = tab |> getProto |> getProto
        sendMode = messageSendMode.Normal
    } |> context_sendMessage messageQuery

and internal messageNotReceived (messageQuery : messageQuery) (context : objectContext) =
    failwith "Message not received"

let create = Object.toInstance

let createAndInit (messageQuery : messageQuery) (tab : table) =
    let instance = tab |> create
    instance |> sendMessage messageQuery |> ignore
    instance

let rec instanceOf (class' : table) (instance' : table) =
    match instance' |> tryFindProto with
    | Some proto ->
        if (proto |> Meta.get Meta.Keys.id) = (class' |> Object.getPrototype |> Meta.get Meta.Keys.id) then true
        else proto |> instanceOf class'
    | _ -> false

let tryGet (key : string) (tab : table) =
    match tab |> tryFindMember (key |> Member.memberQuery.Get) with
    | Some mi -> Some mi.member'
    | _ -> None

let get (key : string) (tab : table) =
    match tab |> tryGet key with
    | Some m -> m
    | _ -> null