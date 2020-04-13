namespace Sage.Core.Object.Prototyped

open System
open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message
open Member

[<AutoOpen>]
module Operators =
    let (==>) a b = a, Just(box b)

    let (!!) a = a, Undefined

    let (!) a = a, Placeholder

    let (<?) (tab : table) (key : key) =
        Instance.sendMessage (messageQuery.UnaryMessage key) tab

    let (<==) (tab : table) (key : key, value : value messageOption) =
        Instance.sendMessage (messageQuery.BinaryMessage(key, value)) tab

    let (<--) (tab : table) (keywordSeq : optKeywordSeq) =
        Instance.sendMessage (messageQuery.KeywordMessage (keywordSeq)) tab