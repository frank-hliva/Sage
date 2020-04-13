module Sage.Memory
    
open StdLib
open System
open System.Runtime.CompilerServices
    
type TypeInfo = { tag: TypeTag; typeDef: Type; id : string }

and TypeTag =
| Undefined = 0
| Bool = 0
| Byte = 1
| SByte = 2
| Int16 = 3
| UInt16 = 4
| Int = 5
| UInt32 = 6
| Int64 = 7
| UInt64 = 8
| NativeInt = 9
| UNativeInt = 10
| Char = 11
| String = 12
| Decimal = 13
| Unit = 14
| Float32 = 15
| Float = 16
| Complex = 63


let baseTypeTable =
    [
        { tag = TypeTag.Undefined; typeDef = null; id = "undefined" }
        { tag = TypeTag.Bool; typeDef = typedefof<bool>; id = "bool" }
        { tag = TypeTag.Byte; typeDef = typedefof<byte>; id = "byte" }
        { tag = TypeTag.SByte; typeDef = typedefof<sbyte>; id = "sbyte" }
        { tag = TypeTag.Int16; typeDef = typedefof<int16>; id = "int16" }
        { tag = TypeTag.UInt16; typeDef = typedefof<uint16>; id = "uint16" }
        { tag = TypeTag.Int; typeDef = typedefof<int>; id = "int" }
        { tag = TypeTag.UInt32; typeDef = typedefof<uint32>; id = "uint32" }
        { tag = TypeTag.Int64; typeDef = typedefof<int64>; id = "int64" }
        { tag = TypeTag.UInt64; typeDef = typedefof<uint64>; id = "uint64" }
        { tag = TypeTag.NativeInt; typeDef = typedefof<nativeint>; id = "nativeint" }
        { tag = TypeTag.UNativeInt; typeDef = typedefof<unativeint>; id = "unativeint" }
        { tag = TypeTag.Char; typeDef = typedefof<char>; id = "char" }
        { tag = TypeTag.String; typeDef = typedefof<string>; id = "string" }
        { tag = TypeTag.Decimal; typeDef = typedefof<decimal>; id = "decimal" }
        { tag = TypeTag.Unit; typeDef = typedefof<unit>; id = "unit" }
        { tag = TypeTag.Float32; typeDef = typedefof<float32>; id = "float32" }
        { tag = TypeTag.Float; typeDef = typedefof<float>; id = "float" }
        { tag = TypeTag.Complex; typeDef = null; id = "complex" } 
    ]


let baseTypeTableMap = 
    baseTypeTable
    |> Seq.map(fun typeInfo -> typeInfo.tag, typeInfo)
    |> Map


