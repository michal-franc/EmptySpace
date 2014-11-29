module Ship

open Storage
open CrewList

open Xunit
open FsUnit.Xunit

type Ship = {
    CrewList : CrewList 
    Storage : Storage
 }

let createStarterShip :Ship = 
    {
        CrewList = CrewList.createDefault
        Storage = Storage.createDefault 
    }

let tick ship :Ship = 
    ship

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