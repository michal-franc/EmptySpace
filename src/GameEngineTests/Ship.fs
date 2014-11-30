module Ship

open Storage
open CrewList

open Xunit
open FsUnit.Xunit

type Ship = {
    CrewList : CrewList 
    Storage : Storage
 } with 
    member this.tick =
     let needs = this.CrewList.needs
     match needs with
        | [] -> ignore
        | _ -> needs |> List.iter (fun x -> printf "%s %s" x.[0] x.[1])
     { this with CrewList = this.CrewList.tick }

let createStarterShip :Ship = 
    {
        CrewList = CrewList.createDefault
        Storage = Storage.createDefault 
    }

let generateView ship = 
    let title = "Ship Data"
    let crewList = CrewList.print ship.CrewList
    let storageView = Storage.print ship.Storage
    sprintf """ %s
    Crew List
        %s
    Storage View
        %s
    """ title crewList storageView