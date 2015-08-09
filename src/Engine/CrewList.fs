module CrewList

open Human
open Storage

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