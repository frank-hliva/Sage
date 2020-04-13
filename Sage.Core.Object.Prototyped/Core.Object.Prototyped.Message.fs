namespace Sage.Core.Object.Prototyped.Message

open StdLib
open StdLib.Text
open Sage.Core.Object
open Sage.Core.Object.Prototyped

type method = message -> objectContext -> value

and message =
| UnaryMessage of key
| BinaryMessage of key * value messageOption
| KeywordMessage of keywordOrder

and keywordOrder =
| Exact of optKeywordSeq
| Sorted of optKeywordSeq

and optKeywordSeq = (key * value messageOption) seq
and optKeywordMap = Map<key, value messageOption>

and keywordSeq = (key * value) seq
and keywordMap = Map<key, value>

module KeywordOrder =

    let toSeq = function
    | Exact optKeywordSeq -> optKeywordSeq
    | Sorted optKeywordSeq -> optKeywordSeq

    let toWrapper = function
    | Exact _ -> Exact
    | Sorted _ -> Sorted

module internal Key =

    let toKeyName (key : string) =
        let i = key.Split [|':'|]
        match i.Length with
        | 2 -> i.[0].Trim(), i.[1].Trim()
        | 1 -> i.[0].Trim(), i.[0].Trim()
        | _ -> failwithf "Invalid key \"%s\"" key

module internal Opt =

    let toValue = function
    | Just value -> value
    | _ -> null

module Message =

    module Stamp =

        let ofKvItem (k, v) =
            match v with
            | Placeholder -> k
            | _ -> sprintf "%s:*" k

        let ofKeyword : seq<_> -> _ = 
            Seq.map(ofKvItem)
            >> String.join "|"

        let wrap = function
        | "" -> ""
        | input -> sprintf "(%s)" input

        let wrapWithBrackets = sprintf "[%s]"

    let toMessageStamp = function
    | UnaryMessage key -> key |> Stamp.wrapWithBrackets
    | BinaryMessage (key, _) -> sprintf "[%s=*]" key
    | KeywordMessage order ->
        match order with
        | Exact kws -> kws |> Stamp.ofKeyword |> Stamp.wrap
        | Sorted kws -> kws |> Seq.sortBy(fun (k, v) -> k) |> Stamp.ofKeyword
        |> Stamp.wrapWithBrackets