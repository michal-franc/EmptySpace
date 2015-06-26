module MainMenuView

open IView
open System
open GameState
open ViewType
open Human

type MainMenuView() =
    
    member this.GetView state = 
      sprintf "Year Number : %i\n\n" state.Turn +
      sprintf "Alerts: %s\n\n" "Low Food" +
      sprintf "Info: You are on a %s\n\n" state.Location.print +
      sprintf "Menu\n\n" +
      sprintf "l. %A\n" state.Location.Type +
      sprintf "s. Ship\n" +
      sprintf "S. Storage\n" +
      sprintf "m. Missions\n" +
      sprintf "c. Crew\n" +
      sprintf "\n\n\nesc to close game\n" +
      sprintf "\npress space to end day" 

    member this.HandleKeys (key:ConsoleKeyInfo) state =
        match key.Key, key.Modifiers with 
                   | ConsoleKey.S, ConsoleModifiers.Shift -> { state with CurrentView = Storage }
                   | ConsoleKey.S, _-> { state with CurrentView = Ship }
                   | ConsoleKey.L, _-> { state with CurrentView = Location }
                   | ConsoleKey.M, _ -> { state with CurrentView = Missions }
                   | ConsoleKey.C, _ -> { state with CurrentView = Crew }
                   | ConsoleKey.Spacebar, _ -> state.tick 
                   | ConsoleKey.Escape, _ -> state.tick 
                   | _ -> { state with CurrentView = Menu } 

    interface IView with
        member this.innerLoop state = 
            Console.Write(this.GetView state)
            let key = Console.ReadKey()
            let _state = this.HandleKeys key state 
            _state






