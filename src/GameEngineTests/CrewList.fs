module CrewList

open Human
open Storage
open Xunit
open FsUnit.Xunit

type CrewList = {
    Crew : Human list 
   } with
    member this.tick (storage:Storage) = 
        let mutable _crewList = []
        let mutable _storage = storage 
        for mem in this.Crew do 
             let _mem = mem.tick
             let needs = storage.checkItems _mem.tick.getNeeds
             _crewList <- List.append _crewList [_mem.satisfyNeeds needs] 
             _storage <- Storage.removeItems (Map.ofList(needs)) _storage
        ({ Crew = _crewList }, _storage) 

let createDefault = 
    {
        Crew = [ Human.create "Steve"; Human.create "Arnold"; Human.create "Matt"; Human.create "Michael"; Human.create "Thomas" ]
    }

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