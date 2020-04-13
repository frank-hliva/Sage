let add = func a b -> a + b

let minusOne = func a -> (
    let b = 5
    a - 2 - b
)

let add5minus1 = add 5 >> minusOne

alert(4 + 5 |> add5minus1 |> add 4)

alert <| (
    let x = 5
    x * x
)