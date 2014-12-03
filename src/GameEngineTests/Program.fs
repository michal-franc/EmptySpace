open System
open GameState

[<EntryPoint>]
let main argv = 
    let rec mainLoop(gamestate:GameState) = 
        Console.Clear()

        Console.Write(GameState.getView gamestate)

        let key = Console.ReadKey()

        let _gamestate = GameState.keyHandle gamestate key.Key
        mainLoop(_gamestate.tick)    
    
    mainLoop(initialGameState)     
    0
