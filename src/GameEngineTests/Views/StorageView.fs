module StorageView

open IView
open System
open GameState
open ViewType

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


