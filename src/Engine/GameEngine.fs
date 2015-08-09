module GameEngine

open StarSystem
open SFML.Window
open GalaxyGenerator
open CrewList
open Storage

type GameState = {
     Universe : Universe
     CrewList : CrewList
     Storage : Storage
     PlayerPosition : Vector2f
}

let create = fun () ->
    let universe = GalaxyGenerator.generate 
    { 
        PlayerPosition = universe.Systems.Head.Position; 
        Universe = universe; 
        Storage = Storage.createDefault; 
        CrewList = CrewList.createDefault 
    }

let tick state =
    let (crewlist, storage) = state.CrewList.tick(state.Storage)
    { state with Storage = storage; CrewList = crewlist }

let travel state pos = 
    { state with PlayerPosition = pos}
