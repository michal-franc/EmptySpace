open System
open GameState

[<EntryPoint>]
let main argv = 
    let rec mainLoop(gamestate:GameState) = 
        Console.Clear()

        Console.Write(ViewHandler.getView gamestate)

        let key = Console.ReadKey()

        let _gamestate = ViewHandler.keyHandle gamestate key
        mainLoop(_gamestate.tick)    
    
    mainLoop(initialGameState)     
    0
