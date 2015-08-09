module CrewList

open Xunit
open FsUnit.Xunit
open CrewList

[<Fact>] 
let ``If crew member is hungry and there are items in storage then lower the hunger`` ()=
    let storage = Storage.createDefault
    let sut = { Crew = [ { Human.create "Mike" with Hunger = 30 }]  } 

    let _sut, _storage = sut.tick storage

    _sut.Crew.[0].Hunger |> should equal 1
    _storage.Objects.["Food"] |> should equal 99

[<Fact>] 
let ``If crew member is thirsty and there are items in storage then lower the Thirsty`` ()=
    let storage = Storage.createDefault
    let sut = { Crew = [ { Human.create "Mike" with Thirst = 30 }]  } 

    let _sut, _storage= sut.tick storage

    _sut.Crew.[0].Thirst |> should equal 1

[<Fact>] 
let ``If crew member is thirsty and hungry and there are items in storage then lower the Thirsty and Hungry`` ()=
    let storage = Storage.createDefault
    let sut = { Crew = [ { Human.create "Mike" with Thirst = 30; Hunger = 30 }]  } 

    let _sut, _storage= sut.tick storage

    _sut.Crew.[0].Hunger|> should equal 1
    _sut.Crew.[0].Thirst |> should equal 1

let print crewList =
    let s = crewList.Crew 
                    |> List.fold (fun previous crew -> previous + crew.print  + "\n") ""
    sprintf "%s" s