module ListView

open IView
open System
open ViewType
open Human
open libtcod
open TopBar

// TODO:
// This will be an abstract list control with multi selecton option
// On input it gets a list of objects with id and how to display them and a rule if it is non selectable ( greyed out or something like that )
// On output it will give back a list of ids
// on scrolling through elements in the right panel there will be details of selected object

type ListSelection = NotSelected | Selected | Highlighted | HighlightedSelected

type ListElem = {
    Id : int
    Selection : ListSelection
    DisplayText : string
 } 

type ListView() =

    let printItem x symb =
        sprintf "\n%s%i-%s" symb x.Id x.DisplayText
    
    let unhighlightCurrent x  =
        { x with Selection =  match x.Selection with
                              | Highlighted -> NotSelected
                              | HighlightedSelected -> Selected
                              | _ -> x.Selection }

    let highlightFirst list = 
        match list with
         | x :: tail ->  { x with Selection = match x.Selection with
                                              | NotSelected -> Highlighted
                                              | Selected -> HighlightedSelected
                                              | _ -> NotSelected }
                         :: tail
         | [] -> []

    let rec highlightNext list =
        let exists = list |> List.exists (fun x -> match x.Selection with
                                                            | Highlighted -> true
                                                            | HighlightedSelected -> true
                                                            | _ -> false)
        // i think its time to end the first ever stream of live coding by me
        // soon i might add my beatifull voice
        // you can check my blog www.mfranc.com :)
        // see ya 
        if exists then 
               match list with
                | x :: tail -> unhighlightCurrent x :: highlightNext tail
                | [] -> []
        else list |> highlightFirst
           
    let highlightPrevious list = 
        List.rev (highlightNext (List.rev list))
        
    let selectHighlighted list = 
        list |> List.map (fun x ->  { x with Selection = match x.Selection with
                                                         | Highlighted -> HighlightedSelected
                                                         | HighlightedSelected -> Highlighted
                                                         | _ -> x.Selection })

    member this.DrawView list = 
        TCODConsole.root.clear()
        topBarText "List"
        let orderedCrewList = (list |> List.fold (fun acc elem -> 
                                                            match elem.Selection with 
                                                            | NotSelected -> acc + printItem elem ""
                                                            | Selected -> acc + printItem elem "@"
                                                            | HighlightedSelected -> acc + printItem elem "-@"
                                                            | Highlighted -> acc + printItem elem "-"
                                                            ) (""));
        TCODConsole.root.print(1, 5, "Select crew member :" + orderedCrewList)
        TCODConsole.root.print(1, 12, "j. go down k. go up s. select f. finish c. cancel")


        let detailDrawFunc = (fun obj -> sprintf "name: %s\nage: %i\nskill:%i" obj.DisplayText 20 50)

        // Oops :D but it kinda works
        // i still need to figure out how i want to define layouts manual x, y selection is worthless
        // ideally it would be great to have a system that would layout everything on its own
        // libtcod is supporting this in a way

        // BOOM

        TCODConsole.root.print(25, 6, detailDrawFunc (list |> List.find (fun x -> x.Selection = Highlighted || x.Selection = HighlightedSelected )))

        TCODConsole.flush()

        // on each selected item i want to draw details of this item in the left side of the screen
        // 1. stubbed name of the crewmember in the left side
        // 2. injected function that will define how to build this left side element

    member this.loop list = 
        
        this.DrawView list
        // meh so many injects ... will have to change it

        let key = TCODConsole.checkForKeypress()
        match key.Character with 
        | 'j' -> this.loop (highlightNext list)
        | 'k' -> this.loop (highlightPrevious list)
        | 's' -> this.loop (selectHighlighted list)
        | 'c' -> [] //cancel just pop out empty list
        | 'f' -> list |> List.filter (fun x -> x.Selection = Highlighted || x.Selection = HighlightedSelected )
        | _ -> this.loop list 
        

    interface IView with

        member this.innerLoop state = 


            let initialList = state.Ship.CrewList.Crew 
                              |> List.map(fun x -> { Id = x.Id; DisplayText = x.Name; Selection = NotSelected })

            let listWithFirstHighlited = { initialList.[0] with Selection = Highlighted } :: (List.tail initialList)

            let idList = this.loop listWithFirstHighlited
                         |> List.filter (fun x -> x.Selection = Selected || x.Selection = HighlightedSelected ) 
                         |> List.map (fun x -> x.Id)

            let newList = state.Ship.CrewList.Crew 
                          |> List.map (fun crew -> match crew.Id with 
                                                   | id when idList |> List.exists (fun id1 -> id1 = id) -> { crew with Action = Exploring }
                                                   | _ -> crew )
            // here i will want to just return a list of selected items ( this list will be generic )
            { state with CurrentView = Menu; Ship = { state.Ship with CrewList = { Crew = newList }}}