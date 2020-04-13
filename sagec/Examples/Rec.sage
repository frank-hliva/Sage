let loop = func x max -> (
    if x = max then ()
    else (
        alert x
        let y = 1
        loop (x + y) max
    )
)

loop 0 5