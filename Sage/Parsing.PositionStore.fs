namespace Sage.Parsing.ParserInfo

open Sage
open System
open System.Threading

type private PositionStoreMessage =
| Reset
| SetPosition of position
| GetPosition of AsyncReplyChannel<position>
| Stop

type PositionStore() =
    let cancellationTokenSource = new CancellationTokenSource()

    let body (inbox : MailboxProcessor<PositionStoreMessage>) =
        let rec loop (pos : position) = async {
            let! msg = inbox.Receive()
            match msg with
            | Reset ->
                return! loop <| Position.empty
            | SetPosition pos ->
                return! loop(pos)
            | GetPosition replyChannel ->
                replyChannel.Reply(pos)
                return! loop pos
            | Stop ->
                return ()
        }
        loop <| Position.empty

    let mbox = MailboxProcessor.Start(body, cancellationTokenSource.Token)

    member c.Reset() =
        mbox.Post(Reset)

    member c.SetPosition(pos : position) =
        mbox.Post(SetPosition pos)

    member c.GetPosition() =
        mbox.PostAndReply((fun reply -> GetPosition reply), 1000)

    member c.GetPositionAsync() =
        mbox.PostAndAsyncReply((fun reply -> GetPosition reply), 1000)

    member c.Stop() =
        mbox.Post(Stop)

    interface IDisposable with
        member c.Dispose() = 
            c.Stop()
            (mbox :> IDisposable).Dispose()
            cancellationTokenSource.Cancel()