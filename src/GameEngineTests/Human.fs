module Human

open Xunit
open FsUnit.Xunit

[<Literal>]
let Rate = 1

let mutable counter = 0

type Human = {
   Id : int
   Name : string
   Hunger : int
   Thirst : int
   Health : int
} with
    member this.tick = { this with Hunger = this.Hunger + Rate ; Thirst = this.Thirst + Rate }
    member this.needCheck = 
        let mutable needs = List.Empty
        needs <- if this.Hunger > 30 then List.append needs ["Food"] else []
        needs <- if this.Thirst > 30 then List.append needs ["Water"] else []
        needs


let create name = 
    counter <- counter + 1
    {
       Id = counter
       Name = name
       Hunger = 0
       Thirst = 0
       Health = 100
    }   

[<Fact>] 
let ``Each created human has incremented id`` ()=
    counter <- 0

    let sut = create "a"
    sut.Id |> should equal 1

    let sut = create "b"
    sut.Id |> should equal 2

[<Fact>] 
let ``I can create default Human with Name and all value set to default`` ()=
    let sut = create "Steve"
    sut.Name |> should equal "Steve"
    sut.Hunger |> should equal 0
    sut.Thirst |> should equal 0

[<Fact>] 
let ``On each tick hunger and thirst is modified`` ()=
    let sut = create "Steve"

    let newHuman = sut.tick

    newHuman.Hunger |> should not' (equal 0)
    newHuman.Thirst|> should not' (equal 0)

[<Fact>] 
let ``If Hunger > 30 generate food need and if Thirst > 30 generate water need`` ()=
    let sut = { Hunger = 31; Thirst = 31; Name = ""; Id = 1; Health = 100 }
    let needs = sut.needCheck
    needs.Length |> should equal 2
    needs.[0] |> should equal "Food"
    needs.[1] |> should equal "Water"

