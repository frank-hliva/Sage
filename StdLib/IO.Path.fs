module StdLib.IO.Path

open System.IO

let isDirectory (path : string) =
    ((path |> File.GetAttributes) &&& FileAttributes.Directory) = FileAttributes.Directory

let isFile = isDirectory >> not

let unifySlashes (path : string) = path.Replace("\\", "/")

let unify (path : string) =
    let path = path |> unifySlashes
    if path |> isFile then path
    else 
        if path.EndsWith("/")
        then path.[..path.Length - 2]
        else path

let join (paths : string seq) =
    paths
    |> Seq.fold
        (fun acc path ->
            match (if path.EndsWith(":") then path + "/" else path) |> unifySlashes with
            | path when acc = "" -> path
            | path when path.StartsWith("/") -> path.[1..]
            | _ -> path
            |> fun path -> System.IO.Path.Combine(acc, path) |> unifySlashes) ""

let split (path : string) =
    (path |> unify).Split([|'/'|]) |> seq

let parent (level : int) (path : string) =
    let items = path |> split |> List.ofSeq
    items.[..items.Length - (1 + level)] |> join