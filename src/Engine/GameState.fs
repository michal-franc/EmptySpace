module GameState

open Ship
open System
open StarSystem

type GameState = {
     GameSpeed : int
     Universe : Universe
     Ship : Ship
     Date : DateTime
}

