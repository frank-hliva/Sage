module App

module Math (
    let plus = func a b -> a + b
    let minus = func a b -> a - b
    module Test (
        module Test1 (
            let plus5 = func a -> plus a 5
            let plus10 = plus 10
            let print = func -> say(plus 1 1)
            let print20 = func -> say(plus10 10)
        )
    )
)

Math.Test.Test1.print() #vypise 2
say(Math.Test.Test1.plus5 10) #vypise 15
say(Math.Test.Test1.plus10 2) #vypise 12
say(Math.Test.Test1.print20 ()) #vypise 20

((((((App :?> table).["Modul1"] :?> table).["Modul2"] :?> table).["Module3"] :?> table).["Module4"] :?> table).print :?> (string -> unit)) "Hello World"