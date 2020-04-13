#r "netstandard"
#r @"c:/Users/hliva/.nuget/packages/fslexyacc.runtime/9.0.2/lib/netstandard2.0\FsLexYacc.Runtime.dll"
#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"
#r @"c:/MyLang/Sage/Sage.Core.Object.Prototyped/bin/Debug/netstandard2.0/Sage.Core.Object.Prototyped.dll"

#load "Core.fs"
#load "Core.Types.fs"
#load "Core.Module.fs"

open System
open System.Collections.Generic
open System.IO
open StdLib.IO
open Sage.Core
open Sage.Core.Object