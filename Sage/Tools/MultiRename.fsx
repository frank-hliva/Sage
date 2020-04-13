#r @"c:\MyLang\Sage\StdLib\bin\Debug\netstandard2.0\StdLib.dll"

open System
open System.IO
open StdLib
open StdLib.Text

let copyAndReplace (destPath : string) (replacements : Replacements) (srcPath : string) =
    srcPath
    |> File.ReadAllText
    |> fun content -> 
        content
        |> Builder.ofString
        |> Builder.replaceAll replacements
        |> Builder.toString
        |> fun content -> File.WriteAllText(destPath, content)

__SOURCE_DIRECTORY__
|> IO.Path.parent 1
|> Directory.EnumerateFiles
|> Seq.iter
    (fun srcPath ->
        let destPath = srcPath.Replace("Raspberry.", "")
        srcPath |> copyAndReplace destPath (seq ["Raspberry" => "Sage"]))