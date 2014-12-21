module MainMenuView

open IView
open System
open GameState
open ViewType

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
      sprintf "Info: %s\n\n" "You are on a planet lz-512" +
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


type ListSelection = NonSelected | Selected | Highlited | HighlitedSelected

type ExploreView() =

    let rec makeNextHighlighted list =
        let exists = list |> List.exists (fun x -> snd x = Highlited || snd x = HighlitedSelected)

        if exists then
           match list with
            | x :: tail -> match x with
                            | _, Highlited -> (fst x, NonSelected) :: (makeNextHighlighted tail)
                            | _, HighlitedSelected -> (fst x, Selected) :: (makeNextHighlighted tail)
                            | _, _ -> x :: (makeNextHighlighted tail)
            | [] -> [] 
        else
            match list with
            | x :: tail -> (fst x, match snd x with
                                    | NonSelected -> Highlited
                                    | Highlited -> NonSelected
                                    | HighlitedSelected -> Selected
                                    | Selected -> HighlitedSelected)
                            :: tail
            | [] -> []

    let makePreviouHighlighted list = 
        List.rev (makeNextHighlighted (List.rev list))
        

    let selectHighlited list = 
        // dont change the fst value but change the snd only if Highlited and HSelected
        list |> List.map (fun x -> (fst x, match snd x with
                                            | Highlited -> HighlitedSelected
                                            | HighlitedSelected -> Highlited
                                            | _ -> snd x))

    member this.GetView list = 
        let orderedCrewList, counter = (list |> List.fold (fun state elem -> 
                                                            let s, counter = state
                                                            let name, selection = elem
                                                            match selection with 
                                                                | NonSelected -> (s + sprintf "\n%i." counter + (name), counter + 1)
                                                                | Selected -> (s + sprintf "\n@%i." counter + (name), counter + 1)
                                                                | HighlitedSelected -> (s + sprintf "\n-@%i." counter + (name), counter + 1)
                                                                | Highlited -> (s + sprintf "\n-%i." counter + (name), counter + 1)
                                                                ) ("", 1))

                                                                                
        "Select crew member :" + orderedCrewList

    member this.loop list = 

        Console.Clear()
        Console.Write(this.GetView list)
        Console.WriteLine("\n\n\n\nj. go down k. go up s. select f. finish c. cancel")

        let key = Console.ReadKey()

        match key.Key with 
                   | ConsoleKey.J -> this.loop (makeNextHighlighted list)
                   | ConsoleKey.K -> this.loop (makePreviouHighlighted list)
                   | ConsoleKey.S -> this.loop (selectHighlited list)
                   | ConsoleKey.C -> [] 
                   | ConsoleKey.F -> list
                   | _ -> this.loop list 

    interface IView with
        member this.innerLoop state = 
            let initialList = state.Ship.CrewList.Crew |> List.map(fun x -> (x.Name, NonSelected) )

            let list = this.loop initialList

            { state with CurrentView = Menu}