namespace StdLib

open System.Threading
open System

type private CounterMessage =
| Set of int64 * AsyncReplyChannel<unit>
| Increment of int64 * AsyncReplyChannel<int64>
| Fetch of AsyncReplyChannel<int64>
| Stop

type Counter(?initialValue : int64) =
    let cancellationTokenSource = new CancellationTokenSource()
    let initialValue = defaultArg initialValue 0L

    let body (inbox : MailboxProcessor<CounterMessage>) =
        let rec loop (n : int64) = async {
            let! msg = inbox.Receive()
            match msg with
            | Set(v, replyChannel) ->
                replyChannel.Reply()
                return! loop(v)
            | Increment(v, replyChannel) ->
                let incrementedValue = n + v
                replyChannel.Reply(incrementedValue)
                return! loop(incrementedValue)
            | Fetch replyChannel ->
                replyChannel.Reply(n)
                return! loop(n)
            | Stop ->
                return ()
        }
        loop initialValue

    let mbox = MailboxProcessor.Start(body, cancellationTokenSource.Token)

    member c.Reset() =
        mbox.PostAndReply(fun reply -> Set(initialValue, reply))

    member c.SetValue(value : int64) =
        mbox.PostAndReply(fun reply -> Set(value, reply))

    member c.Increment(?value : int64) =
        mbox.PostAndReply(fun reply -> Increment(defaultArg value 1L, reply))

    member c.Decrement(?value : int64) =
        c.Increment(-(defaultArg value 1L))

    member c.IncrementAsync(?value : int64) =
        mbox.PostAndAsyncReply(fun reply -> Increment(defaultArg value 1L, reply))

    member c.DecrementAsync(?value : int64) =
        c.IncrementAsync(-(defaultArg value 1L))

    member c.Fetch() =
        mbox.PostAndReply(fun reply -> Fetch reply)

    member c.FetchAsync() =
        mbox.PostAndAsyncReply(fun reply -> Fetch reply)

    member c.Stop() =
        mbox.Post(Stop)

    interface IDisposable with
        member c.Dispose() = 
            c.Stop()
            (mbox :> IDisposable).Dispose()
            cancellationTokenSource.Cancel()