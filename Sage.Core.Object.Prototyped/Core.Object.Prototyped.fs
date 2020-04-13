namespace Sage.Core.Object.Prototyped

open Sage.Core.Object

type keyValueSeq = (key * value) seq

type messageOption<'t> =
| Just of 't
| Undefined
| Placeholder

type objectContext = {
    self : table
    super : table
    sendMode : messageSendMode
}

and messageSendMode =
| Normal = 0
| Self = 1
| Super = 2

type Target = 
| Prototype = 0
| Object = 1