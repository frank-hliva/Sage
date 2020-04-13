namespace Sage.Parsing.ParserInfo

open Sage
open Sage.LexicalAnalysis
open System
open System.Threading

type TokenInfo = Token option

type private TokenStoreMessage =
| Reset
| SetToken of Token
| GetCurrentToken of AsyncReplyChannel<TokenInfo>
| Stop

type TokenStore() =
    let cancellationTokenSource = new CancellationTokenSource()

    let body (inbox : MailboxProcessor<TokenStoreMessage>) =
        let rec loop (token : TokenInfo) = async {
            let! msg = inbox.Receive()
            match msg with
            | Reset ->
                return! loop None
            | SetToken token ->
                return! loop(Some token)
            | GetCurrentToken replyChannel ->
                replyChannel.Reply(token)
                return! loop token
            | Stop ->
                return ()
        }
        loop None

    let mbox = MailboxProcessor.Start(body, cancellationTokenSource.Token)

    member c.Reset() =
        mbox.Post(Reset)

    member c.SetToken(token : Token) =
        mbox.Post(SetToken token)

    member c.GetCurrentToken() =
        mbox.PostAndReply((fun reply -> GetCurrentToken reply), 1000)

    member c.GetCurrentTokenAsync() =
        mbox.PostAndAsyncReply((fun reply -> GetCurrentToken reply), 1000)

    member c.Stop() =
        mbox.Post(Stop)

    interface IDisposable with
        member c.Dispose() = 
            c.Stop()
            (mbox :> IDisposable).Dispose()
            cancellationTokenSource.Cancel()