type TypeTag =
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
        TypeTag.Bool, typedefof<bool>, "bool" 
        TypeTag.Byte, typedefof<byte>, "byte" 
        TypeTag.SByte, typedefof<sbyte>, "sbyte" 
        TypeTag.Int16, typedefof<int16>, "int16" 
        TypeTag.UInt16, typedefof<uint16>, "uint16" 
        TypeTag.Int, typedefof<int>, "int" 
        TypeTag.UInt32, typedefof<uint32>, "uint32" 
        TypeTag.Int64, typedefof<int64>, "int64" 
        TypeTag.UInt64, typedefof<uint64>, "uint64" 
        TypeTag.NativeInt, typedefof<nativeint>, "nativeint" 
        TypeTag.UNativeInt, typedefof<unativeint>, "unativeint" 
        TypeTag.Char, typedefof<char>, "char" 
        TypeTag.String, typedefof<string>, "string" 
        TypeTag.Decimal, typedefof<decimal>, "decimal" 
        TypeTag.Unit, typedefof<unit>, "unit" 
        TypeTag.Float32, typedefof<float32>, "float32" 
        TypeTag.Float, typedefof<float>, "float" 
    TypeTag.Complex, null, null, null 
    ]


let baseTypeTableMap = 
    baseTypeTable
    |> Seq.map(fun (tag, typeDef, nativeName) -> tag, (tag, typeDef, nativeName))
    |> Map


