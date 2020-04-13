module StdLib.Bit

let get index (byte : byte) = byte &&& (1uy <<< index) <> 0uy

let set index (value : bool) (byte : byte) = 
    if value then byte ||| (1uy <<< index)
    else byte &&& ~~~(1uy <<< index)