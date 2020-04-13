let method1 = func {set_name: for:} -> (
    { ...self, name, person, color }
)

Object.empty
|> Object.add (func [init => age:] -> {
        ...self,
        name: "Fero Hliva",
        person: nil,
        hair_color:
        Colors.Red, age
})
|> Object.add (func [set_age:] -> (
    { ...self, age: set_age }
))
|> Object.add func [hair_color:] -> (
    { ...self, hair_color: hair_color }
)
|> Object.add (func [hair_color=] -> ( ## setter / binarna sprava - narozdiel od bežných jazykov setter môže vracať aj výsledok
    self <: { hair_color }
))
|> Object.add (func [hair_color] -> ( ## getter / unarna sprava
    self.hair_color
))
|> Object.add (func [eyes_color] or [eyes_color =] } -> ( ## setter / binarna sprava - narozdiel od bežných jazykov setter môže vracať aj výsledok
    self <: { hair_color }
))
|> Object.add (func [hair_color] -> ( ## getter / unarna sprava
    self.hair_color
))
|> Object.add
    func
    | nastav_rozmery
    | vyska: h is int
    | a
    | sirka: w is int -> ( ## poradie parametrov musí byť dodržané
        
        self <: { vyska, sirka }
    )


## let obj = Object <- init
obj <-
    | add
    | a: 5
    | and
    | b: 4

|> Object.add (func {add:a | plus:b} -> ( ## poradie parametrov musí byť dodržané
    a + b
))

obj.[add: 5 plus: 4]

let person = Person <- init ## objektu Person sa posle sprava create ktora zavola inicializacnu metodu a na zaklade prototypu vytvori instanciu

person <- set_age: 25 ## objektu sa posle sprava typu kluc hodnota
person <- hair_color := Colors.blonde ## objektu sa posle binarna sprava := sa pozuiva ako operator mutable priradenia kedze ide o funkcionalny jazyk a operator = sa pouziva iba na inicializaciu alebo porovnavanie)
let color = person <- hair_color ## objektu sa posle unarna sprava vysledok sa ulozi do premennej color
person <- set_name: "" | for: me => set_color: Colors.red ## objektu person sa pošle jedna (to chcem zdôrazniť) správa zložená z viacerých keywordov na začiatku keywordu je | pred prvým keywordom je spojovnik nepovinný
## koli vačšej prehľadnosti sa dajú keywordy 1 správy zalomiť do riadkov
person <-
    | set_name: ""
    | for: me
    | set_color: Colors.red

person.[
    nastav_rozmery
    vysku: 522
    a
    sirku: 459
] ## správu je možné oblaiť aj do hranatých zátvoriek potom je možné vynechať spojovníky

## kombinované poslanie viacero správ naraz (vykonajú sa po sebe)
person <- 
    | set_name: ""
    | for: _ ## jazyk podporuje aj defaultne parametre bohužiaľ aj pri vynechaní hodnoty sa musí uviesť kľúč (objektový model je tak navrhnutý) bez uvedenia keywordu by išlo o iný typ správy
    | set_color: Colors.red & ## operátor spájania správ &
    | set_age: 25

## to isté čo predchádzajúci prípad akurát sa neprijíma správa ale volá metóda:
## rozdiel medzi poslaním správy a zavolaním metódy je vynechanie medzikroku
## pri poslaní správy správu najprv prijme receiver metóda (receive) ktorá rozhodne čo so správou urobí (defaultne zavolá metódu ale dá sa predefinovať aj iné správanie)
## priame volanie metód je rýchlejšie ale je tam len pre špeciálne prípady a neodporúča sa
## ak pri priamom volaní voláme neexistujúcu metódu nastane chyba 
## ak pošleme objektu správu ktorá nebola nadefinovaná zavolá sa metóda unreceived kde sa môže nedoručená správa ďalej spravcovávať

person ::
    | set_name: ""
    | for: _
    | set_color: Colors.red &
    | set_age: 25

person::hair_color := Colors.red 