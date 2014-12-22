module ExploreView

open IView
open System
open ViewType
open Human

type ListSelection = NonSelected | Selected | Highlited | HighlitedSelected

type ExploreView() =

    let highlightFirst list = 
        match list with
         | (fst, snd, thd) :: tail -> (fst, snd, match thd with
                                                 | NonSelected -> Highlited
                                                 | Selected -> HighlitedSelected
                                                 | _ -> NonSelected)
                                     :: tail
         | [] -> []
    
    let unhighlightCurrent (fst, snd, thd) =
        (fst, snd, match thd with
                    | Highlited -> NonSelected
                    | HighlitedSelected -> Selected
                    | _ -> thd)

    let rec makeNextHighlighted list =
        let exists = list |> List.exists (fun (_, _, x) -> match x with
                                                            | Highlited -> true
                                                            | HighlitedSelected -> true
                                                            | _ -> false)

        if exists then 
               match list with
                | x :: tail -> unhighlightCurrent x :: makeNextHighlighted tail
                | [] -> []
        else list |> highlightFirst
           

    let makePreviouHighlighted list = 
        List.rev (makeNextHighlighted (List.rev list))
        

    let selectHighlited list = 
        list |> List.map (fun (fst, snd, thd) ->  (fst, snd, match thd with
                                                              | Highlited -> HighlitedSelected
                                                              | HighlitedSelected -> Highlited
                                                              | _ -> thd))

    member this.GetView list = 
        let orderedCrewList, counter = (list |> List.fold (fun state elem -> 
                                                            let s, counter = state
                                                            let id, name, selection = elem
                                                            match selection with 
                                                             | NonSelected -> (s + (sprintf "\n%i. %i-" counter id) + (name), counter + 1)
                                                             | Selected -> (s + (sprintf "\n@%i. %i-" counter id) + (name), counter + 1)
                                                             | HighlitedSelected -> (s + (sprintf "\n-@%i. %i-" counter id) + (name), counter + 1)
                                                             | Highlited -> (s + (sprintf "\n-%i. %i-" counter id)+ (name), counter + 1)
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
            let initialList = state.Ship.CrewList.Crew 
                                |> List.map(fun x -> (x.Id, x.Name, NonSelected) )

            let idList = this.loop initialList 
                            |> List.filter (fun (_, _, sel) -> sel = Selected || sel = HighlitedSelected ) 
                            |> List.map (fun (id, _, _) -> id)

            let newList = state.Ship.CrewList.Crew 
                            |> List.map (fun crew -> match crew.Id with 
                                                      | id when idList |> List.exists (fun id1 -> id1 = id) -> { crew with Action = Exploring }
                                                      | _ -> crew )

            { state with CurrentView = Menu; Ship = { state.Ship with CrewList = { Crew = newList }}}