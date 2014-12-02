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
    member this.getNeeds = 
            let mutable needs = []
            needs <- List.append needs (if this.Hunger > 30 then  [("Food", 1)] else [])
            needs <- List.append needs (if this.Thirst > 30 then  [("Water", 1)] else [])
            needs
    member this.satisfyNeeds satisfiedNeeds = 
            let mutable modifiedT = this
            for n in satisfiedNeeds do
                 modifiedT <- match n with 
                                 | "Food" -> { modifiedT with Hunger = modifiedT.Hunger - 30 }
                                 | "Water" -> { modifiedT with Thirst = modifiedT.Thirst - 30 }
                                 | _ -> modifiedT
            modifiedT
            
            

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