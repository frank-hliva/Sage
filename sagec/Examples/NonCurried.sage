let add = func a b -> a + b ## curried function
let addNonCurried = ^func a b -> a + b ## non curried

alert (add 3 7)
alert (^addNonCurried 6 6)

let add2 = add 2
let add4 = add2 >> add2
alert(add4 5)