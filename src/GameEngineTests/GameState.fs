module GameState

open Ship
open System
type View = Menu | Crew | Missions | Storage | Explore

type GameState = {
    Turn : int
    Ship : Ship
    CurrentView : View
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

let keyHandle state key = 
    match state.CurrentView with 
            | Menu -> match key with 
                       | ConsoleKey.NumPad1 -> { state with CurrentView = Missions }
                       | _ -> { state with CurrentView = Menu } 
            | Missions -> match key with 
                            | ConsoleKey.NumPad1 -> { state with CurrentView = Explore }
                            | _ -> { state with CurrentView = Menu } 
            | _ -> { state with CurrentView = Menu } 



let getView state =
    match state.CurrentView with 
                | Menu -> let shipView = Ship.generateView state.Ship
                          sprintf "%s" shipView +
                          sprintf "turn number : %i" state.Turn +
                          sprintf "Actions\n1.Explore" + 
                          sprintf "press any key to turn" 

                | Missions -> "Select Mission" +
                              "1. Explore" 
                | Explore -> "Select crew for explore mission" +
                              "1. Stefan !" 
                | _ -> "Error"