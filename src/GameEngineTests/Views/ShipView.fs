module ShipView

open IView
open System
open ViewType
open GameState
open libtcod
open TopBar

type ShipView() = 
    member this.DrawView state = 
      let shipView = Ship.generateView state.Ship
      topBarText (sprintf "Ship - shields 100 hull 100 speed 1");

    member this.HandleKeys (key: TCODKey) (state: GameState) =
        match key.KeyCode with
        | TCODKeyCode.Escape -> { state with CurrentView = Menu } 
        | _ -> state 

    interface IView with
        member this.innerLoop state = 
            this.DrawView state
            let key = TCODConsole.checkForKeypress()
            let _state = this.HandleKeys key state 
            _state

