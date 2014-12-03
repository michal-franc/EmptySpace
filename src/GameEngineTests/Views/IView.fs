module IView

open GameState
open System

type IView = 
    abstract member GetView: GameState -> string
    abstract member HandleKeys: ConsoleKeyInfo -> GameState -> GameState