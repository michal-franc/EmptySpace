module CrewList

open Human
open Xunit
open FsUnit.Xunit

type CrewList = {
    Crew : Human list 
 } with
    member this.tick =
    { 
        Crew = this.Crew |> List.map (fun mem -> mem.tick)  
    } 

    member this.needs = 
        this.Crew |> List.map (fun mem -> mem.needCheck ) 

let createDefault = 
    {
        Crew = [ Human.create "Steve"; Human.create "Arnold" ]
    }

let print crewList =
    let s = crewList.Crew |> List.fold (fun previous crew -> previous + sprintf "%s hunger : %i thirst : %i" crew.Name crew.Hunger crew.Thirst + "\n") ""
    sprintf "%s" s