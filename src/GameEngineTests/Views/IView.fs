module IView

open GameState
open System

type IView = 
    abstract member innerLoop: GameState -> GameState