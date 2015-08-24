module Events

open SFML.Window
open GameState
open StarSystem
open Destination

let changeSpeed state value  =
    let newValue = match state.GameSpeed + value with
                    | x when x > 6 -> 6
                    | x when x < 0 -> 0
                    | x -> x

    {state with GameSpeed = newValue}


let startTravel state dest = 
    // TODO: move to ship logic
    { state with Ship = { state.Ship with Destination = dest } }

let updateSystem id list f = 
    let rec loop acc = function
        | [] -> acc
        | elem::rest when elem.Id = id -> loop ( f elem :: acc ) rest
        | elem::rest -> loop ( elem::acc ) rest

    loop [] list

let setExplored state id =
    let newSystems = updateSystem id state.Universe.Systems (fun x -> { x with Explored = true })
    // Optimize: looping same collection twice 
    let system = List.find (fun x -> x.Id = id) state.Universe.Systems 

    { state with Universe = { state.Universe with Systems = newSystems }; Alerts = [sprintf "%s %s explored" (state.Date.ToShortDateString()) system.Sun.Name ]}

type Event = 
    | ChangeGameSpeed of factor : int
    | TravelDest of dest: (Vector2f * int)
    | NoStateChange

let apply state = function
    | ChangeGameSpeed(factor) -> changeSpeed state factor
    | TravelDest(dest) -> startTravel state (OnRoute(snd dest, fst dest))
    | NoStateChange -> state


