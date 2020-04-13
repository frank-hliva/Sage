

module My

let plus = func a b -> a + b

let minus = func a b -> a - b

module Calculator a b (
    priv:
    pub let plusA = func x -> x + a
    priv let plusB = func x -> x + b
)

let Calc = Calculator 5 10
say (Calc.plusA 1) #vypise 6
say (Calc.plusB 2) #private - nevypise 12

protocol IMod (
    let printJeSuper = func -> asJeSuper() |> say
    let printJeTypek = func -> asJeTypek() |> say
)

let mod = module Mod1 name : IMod (
    priv:
    let asJeSuper = func -> name + " je super"
    let asJeTypek = func -> name + " je typek"
    pub:
    let printJeSuper = func -> asJeSuper() |> say
    let printJeTypek = func -> asJeTypek() |> say
)

(mod "Fero").printJeSuper ()
(mod "Brano").printJeTypek ()

let Math = module(
    let test = func x -> x
)

say (5 + 5)