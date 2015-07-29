module SystemRenderer

open SFML.Graphics
open SFML.Window
open Helper
open StarSystem

type SpriteText = {
    Sprite : Sprite
    Text : string
}

type SystemRenderer(texture) =
    let mutable sprites : SpriteText list = []

    member this.Sprites with get() = sprites

    member this.sprite pos size t =
        let s = new Sprite(texture, rectForType (rnd()) t)
        s.Scale <- vector1 size 
        s.Position <- adjustPos pos (s.GetGlobalBounds()) |> vector 
        s

    member this.CreateSun(sun) =
        let sunSize = translateSize Sun sun.Size
        let sunPos = (150.0f, 150.0f)
        { Sprite = this.sprite sunPos sunSize PlanetType.Star; Text = sun.Name }

    member this.CreatePlanet(planet:Planet, shiftX, shiftY) =
        let plSize = translateSize Planet planet.Size
        let plPos = (shiftX , shiftY)
        { Sprite = this.sprite plPos plSize planet.Type; Text = planet.Name }

    member this.CreateMoon(moon:Moon, shiftX, shiftY) =
        let mSize = translateSize Moon moon.Size
        let mPos = (shiftX, shiftY)
        { Sprite = this.sprite mPos mSize moon.Type; Text = moon.NameM }

    member this.Create(starSystem:StarSystem) =
        let sun = this.CreateSun(starSystem.Sun)

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