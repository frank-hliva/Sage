#r "netstandard"
#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"

open System
open System.IO
open StdLib
open StdLib.IO

type typeAvailability =
| FSharpAndOcaml = 0
| FSharp = 1
| Ocaml = 2
| Ignore = 3

let types =
    [
        "bool", "Bool", "Boolean", typedefof<bool>, typeAvailability.FSharpAndOcaml
        "byte", "Byte", "Byte", typedefof<byte>, typeAvailability.FSharpAndOcaml
        "sbyte", "SByte", "SByte", typedefof<sbyte>, typeAvailability.FSharpAndOcaml
        "int16", "Int16", "Int16", typedefof<int16>, typeAvailability.FSharpAndOcaml
        "uint16", "UInt16", "UInt16", typedefof<uint16>, typeAvailability.FSharpAndOcaml
        "int", "Int", "Int32", typedefof<int>, typeAvailability.FSharpAndOcaml
        "uint32", "UInt32", "UInt32", typedefof<uint32>, typeAvailability.FSharpAndOcaml
        "int64", "Int64", "Int64", typedefof<int64>, typeAvailability.FSharpAndOcaml
        "uint64", "UInt64", "UInt64", typedefof<uint64>, typeAvailability.FSharpAndOcaml
        "nativeint", "NativeInt", "IntPtr", typedefof<nativeint>, typeAvailability.FSharpAndOcaml
        "unativeint", "UNativeInt", "UIntPtr", typedefof<unativeint>, typeAvailability.FSharpAndOcaml
        "char", "Char", "Char", typedefof<char>, typeAvailability.FSharpAndOcaml
        "string", "String", "String", typedefof<string>, typeAvailability.FSharpAndOcaml
        "decimal", "Decimal", "Decimal", typedefof<decimal>, typeAvailability.FSharpAndOcaml
        "unit", "Unit", null, typedefof<unit>, typeAvailability.FSharpAndOcaml
        "void", "Void", "Void", null, typeAvailability.Ignore
        "single", "Single", "Single", typedefof<single>, typeAvailability.FSharp
        "double", "Double", "Double", typedefof<double>, typeAvailability.FSharp
        "float32", "Float32", "Single", typedefof<float32>, typeAvailability.FSharpAndOcaml
        "float", "Float", "Double", typedefof<float>, typeAvailability.FSharpAndOcaml
    ]

let checkType (value : 'a) = 
    match box value with
    | :? int -> printfn "Int"

let availability = typeAvailability.FSharpAndOcaml

let genTypeInfo = 
    @"module Sage.Memory.Types
    
open StdLib
open System
open System.Runtime.CompilerServices
    
type TypeInfo = { tag: TypeTag; typeDef: Type; id : string }
"

let genBaseTypeEnum types =
    types
    |> List.filter(fun (typeName, upperCase, net, t, a) -> a = availability)
    |> List.mapi(fun i (typeName, upperCase, net, t, availability) -> sprintf "| %s = %d" upperCase i)
    |> fun lines -> (genTypeInfo :: "and TypeTag =" :: "| Undefined = 0" :: lines) @ ["| Complex = 63"; ""; ""]

let genMap = 
    @"let baseTypeTableMap = 
    baseTypeTable
    |> Seq.map(fun typeInfo -> typeInfo.tag, typeInfo)
    |> Map"

let genBaseTypeTable types =
    types
    |> List.filter(fun (typeName, upperCase, net, t, a) -> a = availability)
    |> List.map(fun (typeName, upperCase, net, t, availability) -> sprintf "        { tag = TypeTag.%s; typeDef = typedefof<%s>; id = \"%s\" }" upperCase typeName typeName)
    |> fun lines -> ("let baseTypeTable =" :: "    [" :: "        { tag = TypeTag.Undefined; typeDef = null; id = \"undefined\" }" :: lines) @ ["        { tag = TypeTag.Complex; typeDef = null; id = \"complex\" } "; "    ]"; ""; ""; genMap; ""; ""]

let toFile (lines : (string list) list) =
    let all = lines |> List.concat
    File.WriteAllLines(Path.Combine(__SOURCE_DIRECTORY__, @"Types.fsx"), all)

[
    types |> genBaseTypeEnum
    types |> genBaseTypeTable
]
|> toFile