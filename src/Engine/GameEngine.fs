﻿module GameEngine

open StarSystem
open SFML.Window
open GalaxyGenerator
open CrewList
open VectorHelper
open Storage
open Ship
open System
open GameState

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

let consume events state =
    let rec loop state = function
        | [] -> state
        | event::rest -> loop (Events.apply state event) rest

    loop state events

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
