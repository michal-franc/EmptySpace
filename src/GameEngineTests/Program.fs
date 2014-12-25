open System
open GameState

[<EntryPoint>]
let main argv = 
    let rec mainLoop(gamestate) = 
        let _gamestate = ViewHandler.handle gamestate
        mainLoop(_gamestate)    
    mainLoop(initialGameState)     
    0
