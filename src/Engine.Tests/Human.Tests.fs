module HumanTest

open Xunit
open FsUnit.Xunit
open Human

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

//[<Fact>] 
//let ``Each tick if there is no action decrease Tired`` ()=
//    let sut = { create "Steve" with Action = Nothing; Tired = 50 }
//
//    let newHuman = sut.tick
//
//    newHuman.Tired |> should equal 49

[<Fact>] 
let ``Min Tired value is 0`` ()=
    let sut = { create "Steve" with Action = Nothing; Tired = 0.0f }

    let newHuman = sut.tick

    newHuman.Tired |> should equal 0 