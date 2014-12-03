module MainMenuView

open IView
open System
open GameState
open ViewType

type ShipView() = 
    
    interface IView with 
        member this.GetView state = 
          let shipView = Ship.generateView state.Ship
          sprintf "Overall Condition 100\n" +
          sprintf "%s" shipView

        member this.HandleKeys key state =
            match key.Key with 
                       | _ -> { state with CurrentView = Menu } 

type MainMenuView() =
    
    interface IView with 
        member this.GetView state = 
          sprintf "Year Number : %i\n\n" state.Turn +
          sprintf "Alerts: %s\n\n" "Low Food" +
          sprintf "Info: %s\n\n" "You are on a planet lz-512" +
          sprintf "Menu\n\n" +
          sprintf "s. Ship\n" +
          sprintf "S. Storage\n" +
          sprintf "m. Missions\n" +
          sprintf "c. Crew\n" +
          sprintf "\n\n\n\npress space to end day" 

        member this.HandleKeys key state =
            match key.Key, key.Modifiers with 
                       | ConsoleKey.S, ConsoleModifiers.Shift -> { state with CurrentView = Storage }
                       | ConsoleKey.S, _-> { state with CurrentView = Ship }
                       | ConsoleKey.M, _ -> { state with CurrentView = Missions }
                       | ConsoleKey.C, _ -> { state with CurrentView = Crew }
                       | _ -> { state with CurrentView = Menu } 


type StorageView() =
    
    interface IView with 
        member this.GetView state =
           sprintf "Storage \n%s" (Storage.print state.Ship.Storage)

        member this.HandleKeys key state =
            match key.Key with 
                       | _ -> { state with CurrentView = Menu } 

type CrewView() =
    
    interface IView with 
        member this.GetView state =
           sprintf "CrewList \n%s" (CrewList.print state.Ship.CrewList)

        member this.HandleKeys key state =
            match key.Key with 
                       | _ -> { state with CurrentView = Menu } 

type MissionsView() =
    
    interface IView with 
        member this.GetView state = 
            "Select Mission\n" +
            "1. Explore"

        member this.HandleKeys key state =
            match key.Key with 
                       | ConsoleKey.NumPad1 -> { state with CurrentView = Explore }
                       | _ -> { state with CurrentView = Menu } 