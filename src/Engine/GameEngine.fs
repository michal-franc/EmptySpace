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

    //TODO: Move this whole logic to SHIP tick
    //TODO: BUG - Slight mismatch beetwen dest and position ( it wont reach 0.0f ) -> introduce either error rate or fix the positioning
    let speed = 0.5f;

    let mutable whereToGo = state.PlayerPosition - state.Destination;
    //DOH: this will be prone to bugs hahahah
    if abs whereToGo.X < 0.3f && whereToGo.X <> 0.0f then
        whereToGo <- vector (0.0f, whereToGo.Y)

    if abs whereToGo.Y < 0.3f && whereToGo.Y <> 0.0f then
        whereToGo <- vector (whereToGo.X, 0.0f)

    let moveVector = (normalize whereToGo) * speed;

    if moveVector.X <> 0.0f || moveVector.Y <> 0.0f then
        let (newStorage, x) = Storage.take "Fuel" 0.01f storage
        if x > 0.0f then
            { state with Storage = newStorage; CrewList = crewlist; PlayerPosition = state.PlayerPosition - moveVector }
        else
            { state with Storage = newStorage; CrewList = crewlist; PlayerPosition = state.PlayerPosition }
    else
        { state with Storage = storage; CrewList = crewlist; PlayerPosition = state.PlayerPosition }


let startTravel state pos = 
    { state with Destination = pos}
