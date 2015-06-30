module MainMenuView

open IView
open System
open GameState
open ViewType
open Human
open libtcod
open TopBar

type MainMenuView() =
    
    member this.Draw state = 
      topBarText (sprintf "4501-1-%i" state.Turn);
      TCODConsole.root.setForegroundColor(TCODColor.red);
      TCODConsole.root.print(1, 5, sprintf "Alerts: %s" "Low Food");
      TCODConsole.root.setForegroundColor(TCODColor.white);
      TCODConsole.root.print(1, 6, sprintf "Info: You are in %s" state.Location.print);

      // for instance i can change this to one string with \n
      TCODConsole.root.print(1, 15,sprintf "l. %A\n" state.Location.Type + "s. Ship\nS. Storage\nm. Missions\nc. Crew\nL. list");
      TCODConsole.root.hline(1, 24, 40);
      TCODConsole.root.print(1, 25,sprintf "esc to close game");
      TCODConsole.root.print(1, 26,sprintf "press space to end day");

    member this.HandleKeys (key: TCODKey) (state: GameState) =
        match key.Character, key.KeyCode, key.Shift with
        | 's', _,  true -> { state with CurrentView = Storage }
        | 'L', _, true -> { state with CurrentView = List }
        | 's', _, _-> { state with CurrentView = Ship }
        | 'l', _, _-> { state with CurrentView = System }
        | 'm', _, _ -> { state with CurrentView = Missions }
        | 'c', _, _ -> { state with CurrentView = Crew }
        | _, TCODKeyCode.Space, _ -> state.tick
        | _, TCODKeyCode.Escape, _ -> state.tick
        | _, _, _ -> state 

    interface IView with
        member this.innerLoop state = 
            this.Draw state
            let key = TCODConsole.checkForKeypress()
            let _state = this.HandleKeys key state 
            _state