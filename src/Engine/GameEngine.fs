module GameEngine

open StarSystem
open SFML.Window
open GalaxyGenerator
open CrewList
open Helper
open Storage
open Ship

type GameState = {
     Universe : Universe
     Ship : Ship
}

let create = fun () ->
    let universe = GalaxyGenerator.generate 
    { 
        Ship = Ship.createDefault universe.Systems.Head.Position;
        Universe = universe
    }

let tick state =
    let newShip = state.Ship.tick();
    {state with Ship = newShip}

let startTravel state pos = 
    // TODO: move to ship logic
    { state with Ship = { state.Ship with Destination = pos} }
