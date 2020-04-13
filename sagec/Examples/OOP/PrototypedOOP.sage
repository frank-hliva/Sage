let Parent = ::Object (
    pub func [{init} name:] -> {
        name
    }
    pub func [get_age] -> 33
    pub func [alert_name_with_age] -> (
        alert ("Parent: Name: " + self.name + ", Age: " + self.get_age)
    )
)

let obj = Parent <-
    | init
    | name: "Fero Hliva"



## operator <- a |
## operator .[ ]
## parser definicii sprav