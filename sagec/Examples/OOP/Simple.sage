let fero = (*Person | withName: "Frank Hliva" | andAge: 37) # správa sa dá poslať len instancii ak pošleme správu triede inštania sa sama vytvorí
let sandra = (*Person | withName: "Sandra Belkova" | andAge: 29) # kon+struktorom sa stáva prvá správa ktorá bola objektu zaslaná

#(
    var fero = new Person(withName: "Frank Hliva", andAge: 37)
    var sandra = new Person(withName: "Sandra Belkova", andAge: 29)
)#

*fero | equals: sandra | ifTrue: (
    "Equals"
)
| ifFalse: (
    "Not Equals" 
) &  # objektový pipe operator => po poslaní správy a aplikovaní => sa získa jej výsledok na ktorý sa pošle ďalšia správa
| alert