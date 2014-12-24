module GameState

open Ship
open System
open ViewType

type LocationType = Planet | Space | Asteroid | Station | Unknown

type LocationSize = Huge | Large | Medium | Small | Tiny

type Location = {
    Type : LocationType
    Name : string
    Size : LocationSize
} with 
    member this.print =
            sprintf "%A %A %s" this.Size this.Type this.Name

type GameState = {
    Turn : int
    Ship : Ship
    Location : Location
    CurrentView : ViewType
} with 
    member this.tick = 
       {
        this with Ship = this.Ship.tick; Turn = this.Turn + 1
       }


let initialGameState  = 
    {
        Ship = Ship.createStarterShip
        CurrentView = Menu
        Turn = 0
        Location = {
                   Type = Planet
                   Name = "LZ-415"
                   Size = Medium
        }
    }