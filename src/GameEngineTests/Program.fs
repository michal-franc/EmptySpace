open System
open GameState

let gameTick (gameState:GameState) = 
    {
        gameState with Ship = Ship.tick gameState.Ship
    }

[<EntryPoint>]
let main argv = 


    let rec mainLoop(turn, gamestate) = 
        Console.Clear()

        let shipView = Ship.generateView gamestate.Ship
                
        printfn "%s" shipView
        printfn "turn number : %i" turn 
        printfn "press any key to turn"
        let key = Console.ReadKey()

        mainLoop(turn + 1, gameTick gamestate)    
    
    mainLoop(0, initialGameState)     
    0
