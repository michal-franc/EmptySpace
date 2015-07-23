module SystemRenderer

open SFML.Graphics
open SFML.Window
open Helper
open SpriteWithHint
open StarSystem


type SystemRenderer(texture) =
    let mutable sprites : SpriteWithHint list = []

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

