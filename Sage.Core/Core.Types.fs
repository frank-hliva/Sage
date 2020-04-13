module Sage.Core.Types

open System.Collections.Generic
open Sage.Core.Object

type type' =
| Bool of bool
| Byte of byte
| SByte of sbyte
| Int16 of int16
| UInt16 of uint16
| Int of int
| UInt32 of uint32
| Int64 of int64
| UInt64 of uint64
| NativeInt of nativeint
| UNativeInt of unativeint
| Char of char
| String of string
| Chars of char list
| Decimal of decimal
| Unit of unit
| Float32 of float32
| Float of float
| Nil
| List of type' list
| Seq of type' seq
| Table of table
| Record of IDictionary<type', type'>
| Map of Map<string, type'>
| DefinedType of name : string * type'
| TypeAlias of name : string * type'
| Dynamic of type'