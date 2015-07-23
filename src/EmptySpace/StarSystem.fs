﻿module StarSystem
open SFML.Graphics
open SFML.Window
open Helper
open SpriteWithHint

type ObjectType = Sun | Planet | Moon | Asteroid
type Size = Huge = 1 | Large = 2 | Medium = 3| Small = 4| Tiny =5
type PlanetType = Terran = 1 | Jungle = 2 | Rock = 3| Ocean = 4 | Desert = 6 | Arctic = 7 | Gas = 8 | Inferno = 9 | Toxic = 10 | Star = 11

type Moon = {
    NameM : string
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
   Position : Vector2f
}

    let Font = new Font("ariblk.ttf")

    let translateSize objectType size =
        let multiplier = match objectType with
                         | Sun -> 1.5f
                         | Planet -> 0.5f
                         | Moon -> 0.3f
                         | Asteroid -> 0.2f

        let size = match size with
                   | Size.Huge -> 5.0f
                   | Size.Large -> 4.0f
                   | Size.Medium -> 3.0f
                   | Size.Small -> 2.0f
                   | Size.Tiny -> 1.0f

        size * multiplier
        
    let planetSpriteStart planetType =
        match planetType with
        | PlanetType.Terran -> (79, 73)
        | PlanetType.Jungle -> (79, 110)
        | PlanetType.Rock -> (79, 147)
        | PlanetType.Ocean -> (79, 184)
        | PlanetType.Desert -> (79, 221)
        | PlanetType.Arctic -> (79, 258)
        | PlanetType.Gas -> (79, 295)
        | PlanetType.Inferno -> (79, 332)
        | PlanetType.Toxic-> (79, 369)
        | _ -> (14 , 15)

    let rectForType num planetType =
        let xForNthPlanet = (num - 1) * 37
        let x, y = planetSpriteStart planetType
        new IntRect(x + xForNthPlanet, y, 30, 30)


let rnd = 
    let r = System.Random()
    fun n -> r.Next(1, 6)

type SystemDrawRenderer(filename:string) =
    let image = new Image(filename)
    let mutable texture : Texture = new Texture(image) 
    let mutable sprites : SpriteWithHint list = []
    do
        image.CreateMaskFromColor(Color.Black);
        texture <- new Texture(image)

    member this.Sprites with get() = sprites

    member this.sprite pos size t name =
        let s = new SpriteWithHint(name, texture, rectForType (rnd()) t, Font)
        s.Scale <- vector1 size 
        s.Position <- adjustPos pos (s.GetGlobalBounds()) |> vector 
        s

    member this.CreateSun(sun) =
        let sunSize = translateSize Sun sun.Size
        let sunPos = (150.0f, 150.0f)
        this.sprite sunPos sunSize PlanetType.Star sun.Name

    member this.CreatePlanet(planet:Planet, shiftX, shiftY) =
        let plSize = translateSize Planet planet.Size
        let plPos = (shiftX , shiftY)
        this.sprite plPos plSize planet.Type planet.Name

    member this.CreateMoon(moon:Moon, shiftX, shiftY) =
        let mSize = translateSize Moon moon.Size
        let mPos = (shiftX, shiftY)
        this.sprite mPos mSize moon.Type moon.NameM

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