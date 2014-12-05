open System
open GameState

[<EntryPoint>]
let main argv = 
    let rec mainLoop(gamestate:GameState) = 
        let _gamestate = ViewHandler.handle gamestate
        mainLoop(_gamestate.tick)    
    mainLoop(initialGameState)     
    0
