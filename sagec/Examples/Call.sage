let x = func foo x -> foo x

let a = 2

(func -> (
    let y = "Paula berglová"
    alert ("Ahoj: " + y)
))()
let m = (func x -> alert ("x: " + x))

x (if a <> 1 then alert else (
    func x ->
        alert ("x: " + x)
)) "Hello world"

let add = (func a b -> a + b) 5



alert (add 5)