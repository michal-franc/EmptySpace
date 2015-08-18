module SystemRenderer

open SFML.Graphics
open SFML.Window
open VectorHelper
open StarSystem

type SystemRenderer(texture) =
    member this.sprite pos size t =
        let s = new Sprite(texture, rectForType (rnd()) t)
        s.Scale <- vector1 size 
        s.Position <- adjustPos pos (s.GetGlobalBounds()) |> vector 
        s

    member this.CreateSunSprite(sun) =
        let sunSize = translateSize Sun sun.Size
        let sunPos = (150.0f, 150.0f)
        this.sprite sunPos sunSize PlanetType.Star

    member this.CreatePlanetSprite(planet:Planet, shiftX, shiftY) =
        let plSize = translateSize Planet planet.Size
        let plPos = (shiftX , shiftY)
        this.sprite plPos plSize planet.Type

    member this.CreateMoonSprite(moon:Moon, shiftX, shiftY) =
        let mSize = translateSize Moon moon.Size
        let mPos = (shiftX, shiftY)
        this.sprite mPos mSize moon.Type