module Human

open Xunit
open FsUnit.Xunit

type Action = Exploring | Nothing

[<Literal>]
let Rate = 1

let mutable counter = 0

type Human = {
   Id : int
   Name : string
   Hunger : int
   Thirst : int
   Health : int
   Tired : int
   Action : Action
} with
    member this.tick = 
        let tiredRate = match this.Action with
                        | Nothing -> 0
                        |  _ -> 1
        let tiredValue = this.Tired + tiredRate 

        { this with Hunger = this.Hunger + Rate ; Thirst = this.Thirst + Rate; Tired = if tiredValue < 0 then 0 else tiredValue }
    member this.getNeeds = 
            let mutable needs = []
            needs <- List.append needs (if this.Hunger > 30 then  [("Food", 1)] else [])
            needs <- List.append needs (if this.Thirst > 30 then  [("Water", 1)] else [])
            needs
    member this.satisfyNeeds satisfiedNeeds = 
            let mutable modifiedT = this
            for name, value in satisfiedNeeds do
                 modifiedT <- match name with 
                                 | "Food" -> { modifiedT with Hunger = modifiedT.Hunger - 30 }
                                 | "Water" -> { modifiedT with Thirst = modifiedT.Thirst - 30 }
                                 | _ -> modifiedT
            modifiedT
    member this.print =
            sprintf "%s hunger : %i thirst : %i tired : %i action: %s" this.Name this.Hunger this.Thirst this.Tired (match this.Action with
                                                                                                                    | Exploring-> "Explore"
                                                                                                                    | _ -> "None")
            
            

let create name = 
    counter <- counter + 1
    {
       Id = counter
       Name = name
       Hunger = 0
       Thirst = 0
       Tired = 0
       Health = 100
       Action = Nothing
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
let ``Each tick if there is an action increse Tired`` ()=
    let sut = { create "Steve" with Action = Exploring}

    let newHuman = sut.tick

    newHuman.Tired |> should equal 1

[<Fact>] 
let ``Each tick if there is no action decrease Tired`` ()=
    let sut = { create "Steve" with Action = Nothing; Tired = 50 }

    let newHuman = sut.tick

    newHuman.Tired |> should equal 49

[<Fact>] 
let ``Min Tired value is 0`` ()=
    let sut = { create "Steve" with Action = Nothing; Tired = 0 }

    let newHuman = sut.tick

    newHuman.Tired |> should equal 0 