#light "off"
module Test11 =
begin
    let emit (str : string) = begin () end
    let print (value : 't) = begin emit "console.log(value);\n" end
    let add = fun a b -> begin
    emit "xxx";
    let c = 1 in
    ((a + b) + c)
    end
end

do printfn "%d" (Test11.add 5 3)