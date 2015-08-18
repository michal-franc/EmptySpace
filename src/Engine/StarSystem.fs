module StarSystem
open SFML.Graphics
open SFML.Window
open VectorHelper

type ObjectType = Sun | Planet | Moon | Asteroid
type Size = Huge = 1 | Large = 2 | Medium = 3| Small = 4| Tiny =5

type PlanetType = Terran = 1 | Jungle = 2 | Rock = 3| Ocean = 4 | Desert = 6 | Arctic = 7 | Gas = 8 | Inferno = 9 | Toxic = 10 | Star = 11

type Moon = {
    mName : string
    Size : Size
    Type : PlanetType
    Description : string
}

type Planet = {
    Name : string
    Size : Size
    Type : PlanetType
    Moons : Moon list
    Description : string
}

let createPlanet name size t moons desc =
    { Name = name; Size = size; Type = t; Moons = moons; Description = desc } 

type Sun = {
    Name : string
    Size : Size
    Planets : Planet list
    Color : Color
}

type StarSystem = {
   Sun : Sun
   Position : Vector2f
}

type Universe = {
    Systems : StarSystem list
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
               | _ -> 1.0f

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

let rnd() :int = 
    let r = System.Random()
    r.Next(1, 6)

let rectForType num planetType =
    let xForNthPlanet = (num - 1) * 37
    let x, y = planetSpriteStart planetType
    new IntRect(x + xForNthPlanet, y, 30, 30)