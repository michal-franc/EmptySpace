module GameState

open Ship

type GameState = {
    Ship : Ship
}

let initialGameState :GameState = 
    {
        Ship = Ship.createStarterShip
    }