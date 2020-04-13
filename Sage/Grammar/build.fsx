open System
open System.IO

let toFullPath (fn : string) =
    Path.Combine(__SOURCE_DIRECTORY__, fn)

let toParentFullPath (fn : string) =
    Path.Combine(__SOURCE_DIRECTORY__, "..", fn)

let destPath = @"Parsing.ParserImpl.fsy" |> toParentFullPath
printfn "Creating: %s" destPath
"Parsing.ParserImpl.fsy"
|> toFullPath
|> File.ReadAllLines
|> Seq.map
    (fun line ->
        let kw = @"#include "
        if line.Trim().StartsWith(kw) then
            let fn = line.[kw.Length..].Trim()
            printfn "Adding: %s" fn
            let file = fn |> toFullPath |> File.ReadAllText
            Environment.NewLine + (if file.StartsWith("%%") then file.[2..] else file)
        else line
    )
|> fun lines ->
    File.WriteAllLines(destPath, lines)
    printfn "Success..."