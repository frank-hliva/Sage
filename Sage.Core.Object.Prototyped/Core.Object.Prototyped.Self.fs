namespace Sage.Core.Object.Prototyped

open System
open StdLib
open Sage.Core.Object
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message
open Sage.Core.Object.Prototyped.Instance
open Sage.Core.Object.Prototyped.Member

module Self =
    let sendMessage (selector : messageQuery) (context : objectContext) =
        { context with sendMode = messageSendMode.Self }
        |> Instance.context_sendMessage selector

module Super =
    let sendMessage (selector : messageQuery) (context : objectContext) =
        { context with sendMode = messageSendMode.Super }
        |> Instance.super_sendMessage selector