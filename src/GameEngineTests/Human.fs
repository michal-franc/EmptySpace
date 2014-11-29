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
}

let create name = 
    counter <- counter + 1
    {
       Id = counter
       Name = name
       Hunger = 0
       Thirst = 0
       Health = 100
    }   

let tick human = 
    {
       human with Hunger = human.Hunger + Rate ; Thirst = human.Thirst + Rate 
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

    let newHuman = tick sut

    newHuman.Hunger |> should not' (equal 0)
    newHuman.Thirst|> should not' (equal 0)

