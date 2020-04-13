let aaa = *Person | init | withName: "Fero Hliva" | andAge: 37
let bbb = *Person | init | withName: "Fero Hliva" | andAge: nil 

module Object (
    let addMethod = external
)

let person = *Person | init

let ccc = *Person 
| init
| withName: "Fero Hliva"
| andAge: (fero |> getAge)
| piped: xxx |>
| and: xxx &
| piped1: xxx
| and1: xxxhhh