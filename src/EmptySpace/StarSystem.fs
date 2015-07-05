﻿module StarSystem
open SFML.Graphics
open SFML.Window
open Helper
open TopBar

type ObjectType = Sun | Planet | Moon | Asteroid
type Size = Huge | Large | Medium | Small | Tiny
type PlanetType = Terran | Jungle | Rock | Ocean | Desert | Arctic | Gas | Inferno | Toxic | None

type Moon = {
    Name : string
    Size : Size
    Type : PlanetType
}

type Planet = {
    Name : string
    Size : Size
    Type : PlanetType
    Moons : Moon list
}

type Sun = {
    Name : string
    Size : Size
    Planets : Planet list
}

type StarSystem = {
   Sun : Sun
}
    let Font = new Font("ariblk.ttf")

    let translateSize objectType size =
        let multiplier = match objectType with
                         | Sun -> 1.5f
                         | Planet -> 0.5f
                         | Moon -> 0.3f
                         | Asteroid -> 0.2f

        let size = match size with
                   | Huge -> 5.0f
                   | Large -> 4.0f
                   | Medium -> 3.0f
                   | Small -> 2.0f
                   | Tiny -> 1.0f

        size * multiplier
        
    let planetSpriteStart planetType =
        match planetType with
        | Terran -> (79, 73)
        | Jungle -> (79, 110)
        | Rock -> (79, 147)
        | Ocean -> (79, 184)
        | Desert -> (79, 221)
        | Arctic -> (79, 258)
        | Gas -> (79, 295)
        | Inferno -> (79, 332)
        | Toxic-> (79, 369)
        | _ -> (14 , 15)

    let rectForType num planetType =
        let xForNthPlanet = (num - 1) * 37
        let x, y = planetSpriteStart planetType
        new IntRect(x + xForNthPlanet, y, 30, 30)

 type SpriteWithHint(hint, texture, intRect) =
    inherit Sprite(texture, intRect) with
    let hint = hint

    member this.DrawHint(window:RenderWindow) =
        let mPos = Mouse.GetPosition(window)
        if this.MouseInSprite(mPos) then
            let t, b = this.HintTextRect()
            window.Draw(b)
            window.Draw(t)
        

    member this.MouseInSprite(vector:Vector2i) =
        // cant belevie that it works from the start without any bugs :D
        // this is a little (hack) but it kinda works
        // create a little rect based on mouse pos and then just do a intersect
        // BUG: zoom out ( change in view ) causes this to fail ;)
        let test = new FloatRect(float32(vector.X), float32(vector.Y), 1.0f, 1.0f)
        this.GetGlobalBounds().Intersects(test)

    member this.HintTextRect() =
        let mutable back = new RectangleShape(vector (120.0f, 20.0f))
        back.Position <-  addPos (20.0f, 0.0f) (this.Position.X, this.Position.Y) |> vector
        back.FillColor <- Color.Yellow

        let h = new Text(hint, Font)
        h.Scale <- vector1 (0.4f)
        let pos = centerPos (back.GetGlobalBounds())
        h.Position <- adjustPos pos (h.GetGlobalBounds()) |> vector
        h.Color <- Color.Black

        (h, back)

//TODO: sprite number based on some RNG
type SystemDrawRenderer(filename:string) =

    let texture : Texture = new Texture(filename)
    let mutable sprites : SpriteWithHint list = []
    member this.Sprites with get() = sprites

    member this.sprite pos size t name =
        let s = new SpriteWithHint(name, texture, rectForType 1 t)
        s.Scale <- vector1 size 
        s.Position <- adjustPos pos (s.GetGlobalBounds()) |> vector 
        s

    member this.CreateSun(sun) =
        let sunSize = translateSize Sun sun.Size
        let sunPos = (150.0f, 150.0f)
        this.sprite sunPos sunSize None sun.Name

    member this.CreatePlanet(planet:Planet, shiftX, shiftY) =
        let plSize = translateSize Planet planet.Size
        let plPos = (shiftX , shiftY)
        this.sprite plPos plSize planet.Type planet.Name

    member this.CreateMoon(moon:Moon, shiftX, shiftY) =
        let mSize = translateSize Moon moon.Size
        let mPos = (shiftX, shiftY)
        this.sprite mPos mSize moon.Type moon.Name

    member this.Create(starSystem:StarSystem) =
        let sun = this.CreateSun(starSystem.Sun)

        sprites <- List.append sprites [sun]
        let gap = 30.0f
        let mutable shiftX, shiftY = sun.GetGlobalBounds()
                                     |> centerPos
                                     |> addPos ((sun.GetGlobalBounds().Width / 2.0f) + gap, 0.0f)

        for p in starSystem.Sun.Planets do
            let pl = this.CreatePlanet(p, shiftX, shiftY)
            let mutable mShiftX, mShiftY = pl.GetGlobalBounds()
                                           |> centerPos
                                           |> addPos(0.0f, (pl.GetGlobalBounds().Height / 2.0f) + gap)

            for m in p.Moons do
                let mo = this.CreateMoon(m, mShiftX, mShiftY)
                mShiftY <- mShiftY + mo.GetGlobalBounds().Height + gap
                sprites <- List.append sprites [mo]

            shiftX <- shiftX + (pl.GetGlobalBounds().Width) + gap
            sprites <- List.append sprites [pl]

    interface  Drawable with
        member this.Draw(target, renderState) = 
            for i in sprites do
                target.Draw(i)

type SystemView(system, mainWindow:RenderWindow) =
    let wnd = mainWindow
    let planetsRenderer = new SystemDrawRenderer("planets.png")

    do planetsRenderer.Create(system)

    member this.Render() =
        mainWindow.Draw(new TopBar("Beta Eridani XLS-51", float32(mainWindow.Size.X), Font))
        mainWindow.Draw(planetsRenderer)
        // TODO: Mouse Over has to be handled in sprite somehow so i can hide the complexity
        for s in planetsRenderer.Sprites do
            s.DrawHint(mainWindow)

        // I think its all for now :) the basic sprite with 'hint' is there. At least concept works
        // next step - selectable sprites with injected actions
    //member this.HandleEvents() =
        
