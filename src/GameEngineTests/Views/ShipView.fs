module ShipView

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

