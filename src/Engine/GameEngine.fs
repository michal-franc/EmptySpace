module GameEngine

open StarSystem
open SFML.Window
open GalaxyGenerator
open CrewList
open Helper
open Storage

type GameState = {
     Universe : Universe
     CrewList : CrewList
     Storage : Storage
     PlayerPosition : Vector2f
     Destination : Vector2f
}

let create = fun () ->
    let universe = GalaxyGenerator.generate 
    { 
        PlayerPosition = universe.Systems.Head.Position; 
        Universe = universe; 
        Storage = Storage.createDefault; 
        CrewList = CrewList.createDefault;
        Destination = universe.Systems.Head.Position;
    }

let tick state =
    let (crewlist, storage) = state.CrewList.tick(state.Storage)

    let speed = 0.3f;
    let whereToGo = state.PlayerPosition - state.Destination;
    let moveVector = (normalize whereToGo) * 0.3f;

    { state with Storage = storage; CrewList = crewlist; PlayerPosition = state.PlayerPosition - moveVector }

let startTravel state pos = 
    { state with Destination = pos}
