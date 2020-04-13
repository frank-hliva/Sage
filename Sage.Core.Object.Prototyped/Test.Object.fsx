#r @"c:/MyLang/Sage/StdLib/bin/Debug/netstandard2.0/StdLib.dll"

#load "Core.Object.fs"
#load "Core.Object.Table.fs"
#load "Core.Object.Prototyped.fs"
#load "Core.Object.Prototyped.Message.fs"
#load "Core.Object.Prototyped.Member.fs"
#load "Core.Object.Prototyped.Meta.fs"
#load "Core.Object.Prototyped.Object.fs"
#load "Core.Object.Prototyped.Instance.fs"
#load "Core.Object.Prototyped.Self.fs"
#load "Core.Object.Prototyped.Operators.fs"

open StdLib
open Sage.Core.Object.Table
open Sage.Core.Object.Prototyped
open Sage.Core.Object.Prototyped.Message
open Sage.Core.Object.Prototyped.Member

let MyClass =
    Object.Anonymous.empty()
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "init"
        (fun ctx ->
            ctx.self.["name"] <- "Frank Hliva"
            ctx.self.["age"] <- 37
            ctx.self.["multiplier"] <- 5
        )
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "printAll"
        (fun ctx ->
            printfn "Name: %s" (ctx.self.["name"].ToString())
            printfn "Age: %s" (ctx.self.["age"].ToString())
        )
    |> Object.addMethodForBinaryMessage
        Target.Prototype
        ("printAllWithName" ==> "Gleb")
        (fun msg ctx ->
            printfn "New name: %s" (msg.["printAllWithName"].ToString())
            ctx.self <? "printAll" |> ignore
        )
    |> Object.addMethodForKeywordMessage
        Target.Prototype
        (Sorted ["setName:name" ==> "<setName>"; "age" ==> "<age>"; "newName" ==> "<newname>"])
        (fun msg ctx ->
            ctx.self.["name"] <- msg.["name"]
            ctx.self.["age"] <- msg.["age"]
            ctx.self <== ("printAllWithName" ==> "YYY") |> ignore
            printfn "New name: %s" (msg.["newName"].ToString())
        )
    |> Object.addMethodForKeywordMessage
        Target.Prototype
        (Exact [!"setAndPrint:f"; "name:n" ==> "<name>"; "age:a" ==> "<age>"])
        (fun msg ctx ->
            printfn "setAndPrint-----------------------------------------"
            printfn "self:> %A" ctx.self
            printfn "msg:> %A" msg
            ctx.self.["name"] <- msg.["n"]
            ctx.self.["age"] <- msg.["a"]
            ctx.self <? "printAll" |> ignore
            //printfn "New name: %s" (args.["newName"].ToString())
            printfn "end-------------------------------------------------"
        )
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "test"
        (fun ctx ->
            printfn "Parent"
            ctx |> Self.sendMessage (UnaryMessage "printAll")
        )

    |> Object.addMethodForKeywordMessage
        Target.Prototype
        (Exact [!!"a"; !"+"; !!"b"])
        (fun args ctx ->
            ((args.["a"] :?> int) + (args.["b"] :?> int)) * (ctx.self.["multiplier"] :?> int)
        )

let my = MyClass |> ("init" |> UnaryMessage |> Instance.createAndInit)
my <? "printAll"
my <-- [!"setAndPrint"; "name" ==> "Fero Hliva"; "age" ==> 37]
my <-- ["a" ==> 1;!"+";"b"==> 2]

let my1 = MyClass |> ("init" |> UnaryMessage |> Instance.createAndInit)
my1 <? "printAll"
my1 <-- [!"setAndPrint"; "name" ==> "Sandra"; "age" ==> 29]

my1 <? "printAll"
my <? "printAll"

MyClass |> Object.addMethodForUnaryMessage
    Target.Prototype
    "printAll"
    (fun ctx ->
        printfn "TEST"
        printfn "XName: %s" (ctx.self.["name"].ToString())
        printfn "XAge: %s" (ctx.self.["age"].ToString())
    )

my1 <? "printAll"
my <? "printAll"

let Child = 
    Object.Anonymous.empty()
    |> Object.Anonymous.extends MyClass
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "printAll"
        (fun ctx ->
            printfn "N: %s" (ctx.self.["name"].ToString())
            printfn "A: %s" (ctx.self.["age"].ToString())
            printfn "SELF: %A" ctx.super
        )
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "test"
        (fun ctx ->
            ctx |> Super.sendMessage (UnaryMessage "test")
            printfn "Child1"
        )

let my2 = Child |> ("init" |> UnaryMessage |> Instance.createAndInit)
my2 <== ("printAllWithName" ==> "Sonny")


let Child2 = 
    Object.Anonymous.empty()
    |> Object.Anonymous.extends Child
    |> Object.addMethodForUnaryMessage
        Target.Prototype
        "test"
        (fun ctx ->
            ctx |> Super.sendMessage (UnaryMessage "test")
            printfn "Child2"
            // NEFUNGUJE VOLANIE SUPER CLASS
            //(self |> Object.Instance.getSuper) |> Object.Instance.sendMessageWith (Message.UnaryMessage "printAll") self
        )

let my3 = Child2 |> ("init" |> UnaryMessage |> Instance.createAndInit)
my3 <? "test"

my3 |> Instance.instanceOf MyClass

// ctx aj do inych volani ako super
// premysliet ako vlastne sa maju volat metody