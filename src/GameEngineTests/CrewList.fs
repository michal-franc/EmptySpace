﻿module CrewList

open Human
open Storage
open Xunit
open FsUnit.Xunit

let private checkStorage needs storage =
      let mutable _needs = []
      let mutable _storage = storage
      for need in needs do
            let name, v = need
            let __storage, item = Storage.take name v _storage
            _storage <- __storage
            _needs <- List.append (if item > 0 then [name] else []) _needs

      (_needs, _storage)

type CrewList = {
    Crew : Human list 
   } with
    member this.tick storage = 
        let mutable modifiedCrewList = []
        let mutable _storage = storage
        for mem in this.Crew do 
             let _mem = mem.tick
             let needs = _mem.getNeeds
             let _needs, __storage = checkStorage needs _storage
             _storage <- __storage
             modifiedCrewList <- List.append modifiedCrewList [_mem.satisfyNeeds _needs] 
        ({ Crew = modifiedCrewList }, _storage) 

let createDefault = 
    {
        Crew = [ Human.create "Steve"; Human.create "Arnold" ]
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
    let s = crewList.Crew |> List.fold (fun previous crew -> previous + sprintf "%s hunger : %i thirst : %i" crew.Name crew.Hunger crew.Thirst + "\n") ""
    sprintf "%s" s