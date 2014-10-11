open System
open Player
open SFML.Window
open SFML.Graphics
open MapRenderer

[<EntryPoint>]
let main argv = 

    let tileSize = 20ul
    let windowSize = 800ul
    let frameRate = 60ul
    let howManyTiles = (windowSize / tileSize)

    let player = Player.create 220

    let mainWindow = new RenderWindow(new VideoMode(windowSize, windowSize), "EmptySpace")
    mainWindow.SetFramerateLimit(frameRate)
    mainWindow.Closed.AddHandler(fun sender args -> (sender :?> RenderWindow).Close())

    let viewManipulation key = 
        
        let mainView = mainWindow.DefaultView
        
        match key with
        | Keyboard.Key.H -> mainView.Move(new Vector2f(-50.0f, 0.0f))
        | Keyboard.Key.J -> mainView.Move(new Vector2f(0.0f, 50.0f))
        | Keyboard.Key.K -> mainView.Move(new Vector2f(0.0f, -50.0f))
        | Keyboard.Key.L -> mainView.Move(new Vector2f(50.0f, 0.0f))
        | Keyboard.Key.Comma -> mainView.Rotate(5.0f)
        | Keyboard.Key.Period-> mainView.Rotate(-5.0f)
        | Keyboard.Key.Num9 -> mainView.Zoom(0.9f)
        | Keyboard.Key.Num0 -> mainView.Zoom(1.1f)
        | _ -> ()

        mainWindow.SetView(mainView)

    let mouseManipulations wheelDelta =

        let mainView = mainWindow.DefaultView

        if wheelDelta < 0 then mainView.Zoom(1.1f)
        else mainView.Zoom(0.9f)

        mainWindow.SetView(mainView)

    let optionsKeys key =
        match key with
        | Keyboard.Key.Escape -> mainWindow.Close()
        | _ -> ()

    mainWindow.KeyPressed.AddHandler(fun sender args -> viewManipulation args.Code)
    mainWindow.KeyPressed.AddHandler(fun sender args -> optionsKeys args.Code)

    mainWindow.MouseWheelMoved.AddHandler(fun sender args -> mouseManipulations args.Delta)

    let mapRenderer : MapRenderer = new MapRenderer()

    let world = World.create (howManyTiles * 2ul)  (howManyTiles * 2ul)

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
