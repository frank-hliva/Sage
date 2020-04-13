let fero = (*Person | withName: "Frank Hliva" | andAge: 37) # správa sa dá poslať len instancii ak pošleme správu triede inštania sa sama vytvorí
let sandra = (*Person withName: "Sandra Belkova" | andAge: 29) # konštruktorom sa stáva prvá správa ktorá bola objektu zaslaná

#(
var fero = new Person(withName: "Frank Hliva", andAge: 37)
var sandra = new Person(withName: "Sandra Belkova", andAge: 29)
)#

*"Hello world" printfn

if x = 5 then 10 else *["Hello world"] printfn

*fero equals: sandra
| ifTrue: (
    "Equals"
)
| ifFalse: (
    "Not Equals"
) &  # objektový pipe operator => po poslaní správy a aplikovaní => sa získa jej výsledok na ktorý sa pošle ďalšia správa
| alert

*xxx test = *yyy sys => zzz 5 25

let xxx = "fero hliva"

*[]

*fero
| equals: sandra =>
| ifTrue: "Equals"
| ifFalse: "Not Equals" &  # objektový pipe operator => po poslaní správy a aplikovaní => sa získa jej výsledok na ktorý sa pošle ďalšia správa
| alert 

#( zápis sa dá skrátiť takto )#

*fero
| equals: sandra
=> ifTrue: "Equals" | ifFalse: "Not Equals"
=> alert 

# alebo takto:

(*fero equals sandra => ifTrue: "Equals" | ifFalse: "Not Equals" => alert)


# pri message passingu je možné použiť aj zátvorky

#(
fero.equals(sandra)
.condition(
    ifTrue: () => "Equals",
    ifFalse: () => "Not Equals"
)
.alert()
)#

#(
    Porozmyslat ci by sa predsa nedali pouzit defaultne parametre aj bez pouzitia _
    Proste vygenerovat
)#