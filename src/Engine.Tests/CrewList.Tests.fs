module CrewList

open Xunit
open FsUnit.Xunit
open CrewList

[<Fact>] 
let ``If crew member is hungry and there are items in storage then lower the hunger`` ()=
    let storage = Storage.createDefault
    let sut = { Crew = [ { Human.create "Mike" with Hunger = 30.0f }]  } 

    let _sut, _storage = sut.tick storage

    _sut.Crew.[0].Hunger |> should (equalWithin 0.2) 0.0
    _storage.Objects.["Food"] |> should equal 99.f

[<Fact>] 
let ``If crew member is thirsty and there are items in storage then lower the Thirsty`` ()=
    let storage = Storage.createDefault
    let sut = { Crew = [ { Human.create "Mike" with Thirst = 30.0f }]  } 

    let _sut, _storage= sut.tick storage

    _sut.Crew.[0].Thirst |> should (equalWithin 0.2) 0.0
