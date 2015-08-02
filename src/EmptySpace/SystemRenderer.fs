module SystemRenderer

open SFML.Graphics
open SFML.Window
open Helper
open StarSystem

type SpriteViewModel = {
    Sprite : Sprite
    Name : string
    Description : string
}

let createSpriteViewModel sprite name =
    { Sprite = sprite; Name = name; Description = "" } 

type SystemRenderer(texture) =
    member this.sprite pos size t =
        let s = new Sprite(texture, rectForType (rnd()) t)
        s.Scale <- vector1 size 
        s.Position <- adjustPos pos (s.GetGlobalBounds()) |> vector 
        s

    member this.CreateSun(sun) =
        let sunSize = translateSize Sun sun.Size
        let sunPos = (150.0f, 150.0f)
        createSpriteViewModel (this.sprite sunPos sunSize PlanetType.Star) sun.Name

    member this.CreatePlanet(planet:Planet, shiftX, shiftY) =
        let plSize = translateSize Planet planet.Size
        let plPos = (shiftX , shiftY)
        { createSpriteViewModel (this.sprite plPos plSize planet.Type) planet.Name with Description = planet.Description }

    member this.CreateMoon(moon:Moon, shiftX, shiftY) =
        let mSize = translateSize Moon moon.Size
        let mPos = (shiftX, shiftY)
        createSpriteViewModel (this.sprite mPos mSize moon.Type) moon.NameM 

    member this.Create(starSystem:StarSystem) =
        let sun = this.CreateSun(starSystem.Sun)

        let mutable sprites = []

        sprites <- List.append sprites [sun]
        let gap = 30.0f
        let mutable shiftX, shiftY = sun.Sprite.GetGlobalBounds()
                                     |> centerPos
                                     |> addPos ((sun.Sprite.GetGlobalBounds().Width / 2.0f) + gap, 0.0f)

        for p in starSystem.Sun.Planets do
            let pl = this.CreatePlanet(p, shiftX, shiftY)
            let mutable mShiftX, mShiftY = pl.Sprite.GetGlobalBounds()
                                           |> centerPos
                                           |> addPos(0.0f, (pl.Sprite.GetGlobalBounds().Height / 2.0f) + gap)

            for m in p.Moons do
                let mo = this.CreateMoon(m, mShiftX, mShiftY)
                mShiftY <- mShiftY + mo.Sprite.GetGlobalBounds().Height + gap
                sprites <- List.append sprites [mo]

            shiftX <- shiftX + (pl.Sprite.GetGlobalBounds().Width) + gap
            sprites <- List.append sprites [pl]

        sprites