module CrewView

open IView
open System
open GameState
open ViewType
open Human

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

