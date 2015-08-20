module Events

open SFML.Window
open GameState
open StarSystem

let changeSpeed state value  =
    
    let mutable cnt = state.GameSpeed
    cnt <- cnt + value
    if cnt > 6  then
        cnt <- 6
    if cnt < 0 then
        cnt <- 0

    {state with GameSpeed = cnt}


let startTravel state pos = 
    // TODO: move to ship logic
    { state with Ship = { state.Ship with Destination = pos} }

let updateSystem id list f = 
    let rec loop acc = function
        | [] -> acc
        | elem::rest when elem.Id = id -> loop ( f elem :: acc ) rest
        | elem::rest -> loop ( elem::acc ) rest

    loop [] list

let setExplored state id =
    let newSystems = updateSystem id state.Universe.Systems (fun x -> { x with Explored = true })

    { state with Universe = { state.Universe with Systems = newSystems }}

type Event = 
    | ChangeGameSpeed of factor : int
    | ExploreSystem of systemId : int
    | TravelDest of destPoint : Vector2f
    | NoStateChange

let apply state = function
    | ChangeGameSpeed(factor) -> changeSpeed state factor
    | TravelDest(destPoint) -> startTravel state destPoint
    | ExploreSystem(systemId) -> setExplored state systemId
    | NoStateChange -> state


