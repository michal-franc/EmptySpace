module GameState

open Ship
open System
open ViewType

type GameState = {
    Turn : int
    Ship : Ship
    CurrentView : ViewType
} with 
    member this.tick = 
       {
        this with Ship = this.Ship.tick; Turn = this.Turn + 1
       }


let initialGameState :GameState = 
    {
        Ship = Ship.createStarterShip
        CurrentView = Menu
        Turn = 0
    }