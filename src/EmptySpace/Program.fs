open System
open Player
open SFML.Window
open SFML.Graphics
open MapRenderer

[<EntryPoint>]
let main argv = 

    let tileSize = 20ul
    let windowSize = 800ul
    let frameRate = 40ul
    let howManyTiles = (windowSize / tileSize)

    let player = Player.create 220

    let mainWindow = new RenderWindow(new VideoMode(windowSize, windowSize), "EmptySpace")
    mainWindow.SetFramerateLimit(frameRate)
    mainWindow.Closed.AddHandler(fun sender args -> (sender :?> RenderWindow).Close())

    let view = new View()

    let mapRenderer : MapRenderer = new MapRenderer()

    let world = World.create howManyTiles howManyTiles 

    mapRenderer.Load("tiles.png", new Vector2u(tileSize, tileSize), world.Tiles)

    let rec mainLoop() = 
        mainWindow.Clear()
        mainWindow.DispatchEvents()
        mainWindow.Draw(mapRenderer)
        mainWindow.Display()

        match mainWindow.IsOpen() with
        | true ->  mainLoop() |> ignore
        | false ->  ()
    
    mainLoop()

    0 
