module Destination

open SFML.Window

type Destnation = 
    | None
    | Arrived of systemId : int
    | OnRoute of systemId : int * position : Vector2f
