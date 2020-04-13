namespace Sage

type position =
    {
        Position : int
        Line : int
        Column : int
    }

[<RequireQualifiedAccess>]
module Position =

    let empty =
        {
            Position = 0
            Line = 1
            Column = 1
        }

    let add (pos : position) num =
        { pos with Position = pos.Position + num }

    let addColumn (pos : position) num =
        { pos with
            Position = pos.Position + num
            Column = pos.Column + num }

    let newLine (pos : position) num =
        { pos with
            Position = pos.Position + num
            Line = pos.Line + 1
            Column = 1 }

[<RequireQualifiedAccess>]
module LanguageError =

    let private toErrorString (message : string) (pos : position) =
        sprintf "%s (%d:%d)" message pos.Line pos.Column

    let toError (message : string) (pos : position) =
        pos
        |> toErrorString message