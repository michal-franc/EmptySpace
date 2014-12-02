﻿module Ship

open Storage
open CrewList

open Xunit
open FsUnit.Xunit

type Ship = {
    CrewList : CrewList 
    Storage : Storage
 } with 
    member this.tick =
     let _crewList, _storage = this.CrewList.tick this.Storage
     { this with CrewList = _crewList; Storage = _storage }

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