module MainMenuView

open IView
open System
open GameState
open ViewType
open Human

type ShipView() = 
    member this.GetView state = 
      let shipView = Ship.generateView state.Ship
      sprintf "Overall Condition 100\n" +
      sprintf "%s" shipView

    member this.HandleKeys (key:ConsoleKeyInfo) state =
        match key.Key with 
                   | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state

type MainMenuView() =
    
    member this.GetView state = 
      sprintf "Year Number : %i\n\n" state.Turn +
      sprintf "Alerts: %s\n\n" "Low Food" +
      sprintf "Info: You are on a %s\n\n" state.Location.print +
      sprintf "Menu\n\n" +
      sprintf "s. Ship\n" +
      sprintf "S. Storage\n" +
      sprintf "m. Missions\n" +
      sprintf "c. Crew\n" +
      sprintf "\n\n\n\npress space to end day" 

    member this.HandleKeys (key:ConsoleKeyInfo) state =
        match key.Key, key.Modifiers with 
                   | ConsoleKey.S, ConsoleModifiers.Shift -> { state with CurrentView = Storage }
                   | ConsoleKey.S, _-> { state with CurrentView = Ship }
                   | ConsoleKey.M, _ -> { state with CurrentView = Missions }
                   | ConsoleKey.C, _ -> { state with CurrentView = Crew }
                   | ConsoleKey.Spacebar, _ -> state.tick 
                   | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state


type StorageView() =
    member this.GetView state =
       sprintf "Storage \n%s" (Storage.print state.Ship.Storage)

    member this.HandleKeys (key:ConsoleKeyInfo) state =
        match key.Key with 
                   | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state

type CrewView() =
    member this.GetView state =
           sprintf "CrewList \n%s" (CrewList.print state.Ship.CrewList)

    member this.HandleKeys (key:ConsoleKeyInfo) state =
            match key.Key with 
                       | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state

type MissionsView() =
    member this.GetView state = 
            "Select Mission\n" +
            "e. Explore"

    member this.HandleKeys (key:ConsoleKeyInfo) state =
            match key.Key with 
             | ConsoleKey.E -> { state with CurrentView = Explore }
             | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state