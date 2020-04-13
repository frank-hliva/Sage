namespace StdLib.Text

open System
open System.Globalization
open System.Text

module String =
    let join separator (values : string seq) =
        String.Join(separator, values)



type chars = char list

module Strings =

    let (|Is|_|) (pattern : string) (input : string) =
        if input = pattern then Some()
        else None

[<RequireQualifiedAccess>]
module Chars =

    let toString : chars -> _ = String.Concat

    let ofString : string -> _ = List.ofSeq

    let (|ParseN|_|) (length : int) (input : chars) =
        if input.Length >= length then
            Some(
                (input |> List.take length),
                (input |> List.skip length)
            )
        else None

    let hexToBin (hex : chars) =
        Int32.Parse(hex |> toString, NumberStyles.HexNumber)

type Replacements = (string * string) seq

module Builder =

    let empty = new StringBuilder("")

    let ofString (input : string) = new StringBuilder(input) 

    let toString (builder : StringBuilder) = builder.ToString()

    let replaceAll (replacements : Replacements) (input : StringBuilder) =
        replacements
        |> Seq.fold
            (fun (acc : StringBuilder) (old', new') ->
                acc.Replace(old', new')
            ) input