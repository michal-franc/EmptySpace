module GameEngine

open StarSystem
open SFML.Window
open GalaxyGenerator
open CrewList
open Helper
open Storage
open Ship
open System

type GameState = {
     GameSpeed : int
     Universe : Universe
     Ship : Ship
     Date : DateTime
}

let create = fun () ->
    let universe = GalaxyGenerator.generate 
    { 
        Ship = Ship.createDefault universe.Systems.Head.Position;
        Universe = universe;
        Date = new DateTime(2500, 10, 10, 12, 0, 0);
        GameSpeed = 2;
    }

let tick state =
    let newShip = state.Ship.tick();
    let newDate = state.Date.AddMinutes(1.0);
    {state with Ship = newShip; Date = newDate;}

let startTravel state pos = 
    // TODO: move to ship logic
    { state with Ship = { state.Ship with Destination = pos} }

let changeSpeed state value  =
    
    let mutable cnt = state.GameSpeed
    cnt <- cnt + value
    if cnt > 6  then
        cnt <- 6
    if cnt < 0 then
        cnt <- 0

    {state with GameSpeed = cnt}
       

let speed state =
    match state.GameSpeed with
    | 0 -> 64
    | 1 -> 32
    | 2 -> 16
    | 3 -> 8
    | 4 -> 4
    | 5 -> 2
    | 6 -> 1
    | _ -> 1
