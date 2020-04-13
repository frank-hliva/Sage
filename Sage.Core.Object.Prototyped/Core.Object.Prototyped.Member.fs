module Sage.Core.Object.Prototyped.Member

open StdLib
open StdLib.Text
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message

type memberQuery =
| Get of key
| Set of key * value
| Message of messageQuery

and messageQuery =
| UnaryMessage of key
| BinaryMessage of key * value messageOption
| KeywordMessage of optKeywordSeq

module MessageQuery =

    let toMessages = function
    | messageQuery.UnaryMessage key -> [message.UnaryMessage(key)]
    | messageQuery.BinaryMessage (key, value) -> [message.BinaryMessage(key, value)]
    | messageQuery.KeywordMessage optKeywordSeq ->
        [
            message.KeywordMessage(optKeywordSeq |> Exact);
            message.KeywordMessage(optKeywordSeq |> Sorted)
        ]

module Query =

    let ofMessage = Message

    let toMessage = function
    | memberQuery.Message msg -> msg
    | _ -> failwith "Query is not message"

    let toMessageOption = function
    | memberQuery.Message msg -> Some msg
    | _ -> None

    type memberStampMap = (key * message option) seq

    let toMemberStamps: _ -> memberStampMap = function
    | memberQuery.Get key -> seq [key, None]
    | memberQuery.Set (key, _) -> seq [key, None]
    | memberQuery.Message msg ->
        msg
        |> MessageQuery.toMessages
        |> Seq.map(fun msg -> (msg |> Message.toMessageStamp), Some msg)

type FoundMemberInfo =
    {
        member' : value
        selector : memberQuery
        memberStamps : Query.memberStampMap
        message : message option
    }

let tryFindByStamps (selector : memberQuery) (memberStamps : Query.memberStampMap) (tab : table) =
    memberStamps
    |> Seq.tryPick
        (fun (stamp, optMessage) ->
            match tab |> Table.tryGet stamp with
            | Some member' ->
                {
                    member'= member'
                    selector = selector
                    memberStamps = memberStamps
                    message = optMessage
                } |> Some
            | _ -> None
        )