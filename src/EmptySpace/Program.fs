open System
open Player
open SFML.Window
open SFML.Graphics
open MapRenderer
open StarSystem

[<EntryPoint>]
let main argv = 

    let tileSize = 20ul
    let windowSize = 800ul
    let frameRate = 60ul
    let howManyTiles = (windowSize / tileSize)

    let mainWindow = new RenderWindow(new VideoMode(windowSize, windowSize), "EmptySpace")
    mainWindow.SetFramerateLimit(frameRate)
    mainWindow.Closed.AddHandler(fun sender args -> (sender :?> RenderWindow).Close())

    let optionsKeys key =
        match key with
        | Keyboard.Key.Escape -> mainWindow.Close()
        | _ -> ()

    mainWindow.KeyPressed.AddHandler(fun sender args -> optionsKeys args.Code)

    let test = {
        Sun = {
         Name = "Sol";
         Size = Huge;
         Planets = [{ 
                    Name = "Sol I";
                    Size = Large; 
                    Type = Rock; 
                    Moons = [
                        { Name = "Moon"; Size = Medium; Type = Toxic };
                        { Name = "Moon"; Size = Small; Type = Gas};
                        { Name = "Moon"; Size = Small; Type = Terran};
                        { Name = "Moon"; Size = Small; Type = Arctic};
                        {Name = "Moon";  Size = Small; Type = Gas };
                        {Name = "Moon";  Size = Small; Type = Rock};

                    ]};
                    {
                    Name = "Sol II";
                    Size = Large; 
                    Type = Terran; 
                    Moons = [
                        {Name = "Moon"; Size = Medium; Type = Desert };
                        { Name = "Moon";Size = Small; Type = Gas};
                        {Name = "Moon"; Size = Small; Type = Terran }
                    ]};
                    {
                    Name = "Sol III";
                    Size = Small; 
                    Type = Gas; 
                    Moons = [
                        { Name = "Moon";Size = Medium; Type = Desert};
                        { Name = "Moon";Size = Small; Type = Gas};
                        { Name = "Moon";Size = Small; Type = Toxic}
                    ]};
                    {
                    Name = "Sol IV";
                    Size = Medium; 
                    Type = Rock; 
                    Moons = [
                        { Name = "Moon";Size = Medium; Type = Desert};
                        { Name = "Moon";Size = Small; Type = Gas};
                        { Name = "Moon";Size = Small; Type = Gas};
                    ]};
                    {
                    Name = "Sol V";
                    Size = Small;
                    Type = Terran; 
                    Moons = [
                        { Name = "Moon";Size = Medium; Type = Rock};
                        { Name = "Moon";Size = Small; Type = Gas};
                        { Name = "Moon";Size = Small; Type = Jungle}
                    ]};
                    {
                    Name = "Sol VI";
                    Size = Medium; 
                    Type = Terran; 
                    Moons = [
                        { Name = "Moon";Size = Medium; Type = Inferno };
                        { Name = "Moon";Size = Small; Type = Gas};
                        { Name = "Moon";Size = Small; Type = Ocean}
                    ]};
                    {
                    Name = "Sol VII";
                    Size = Tiny; 
                    Type = Inferno; 
                    Moons = [
                        { Name = "Moon";Size = Medium; Type = Ocean};
                        { Name = "Moon1";Size = Small; Type = Gas};
                        { Name = "Moon2";Size = Small; Type = Rock;}
                    ]};
       ] }
       }

    let systemView = new SystemView(test, mainWindow)

    let rec mainLoop() = 
        mainWindow.Clear()
        mainWindow.DispatchEvents()
        systemView.Render()
        mainWindow.Display()

        match mainWindow.IsOpen() with
        | true ->  mainLoop() |> ignore
        | false ->  ()
    
    mainLoop()

    0 
