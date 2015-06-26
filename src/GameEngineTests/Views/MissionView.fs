module MissionView

open IView
open System
open GameState
open ViewType
open Human  

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