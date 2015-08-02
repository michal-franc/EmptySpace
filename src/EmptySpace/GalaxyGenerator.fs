module GalaxyGenerator
open Universe 
open Helper
open StarSystem
open Microsoft.FSharp.Reflection

let starNames = ["Hastaybos"; "Recleinia"; "Festrilles"; "Xuspade"; "Reyrus"; "Laynov"; "Scecostea"; "Smabeturn"; "Shichi"; "Flillon"]

let longDescLorem = "A desolate planet with rich resources and inidginous life." +
                           "Lorem ipsum dolor sit amet, cu sed summo iudico fastidii." +
                           "Ut pri esse reque dicam, in nemore tincidunt eos. Eu clita" +
                           "periculis complectitur mel, docendi dolores ius no. Et est" +
                           "dicat noster. Et his vitae libris impetus, quem evertitur" +
                           "suscipiantur est at. Ea pertinax efficiendi pro, ubique" +
                           "delectus cum id. suscipiantur est at. Ea pertinax efficiendi " +
                           "pro, ubique";

let intToRoman i =
    match i with 
    | 1 -> "I"
    | 2 -> "II"
    | 3 -> "III"
    | 4 -> "IV"
    | 5 -> "V"
    | 6 -> "VI"
    | 7 -> "VII"
    | 8 -> "VIII"
    | 9 -> "IX"
    | 10 -> "X"
    | _ -> ""


let r = System.Random()

let rndInt min max = 
    r.Next(min, max)

let rnd = fun n -> float32(r.NextDouble())

let rndPos = 
    fun n -> (rnd() * 15000.f, rnd() * 15000.f) |> vector 

let rndColor = fun n ->
    let a = byte (rndInt 0 256)
    let b = byte (rndInt 0 256)
    let c = byte (rndInt 0 256)
    new SFML.Graphics.Color(a, b, c)

let rndType() = 
    enum<PlanetType>(rndInt 1 9)

let rndSize() = 
    enum<Size>(rndInt 1 5)

let rndMoon name =
    { NameM = name; Size = rndSize(); Type = rndType();}

let rndMoons planetName =
    let mutable lis = []
    let nMoons = rndInt 0 3

    for i in 1 .. nMoons do
        let mName = sprintf "%s %s" planetName (intToRoman i)
        lis <- List.append  [rndMoon mName] lis
    lis

let rndPlanet name =
    StarSystem.createPlanet name (rndSize()) (rndType()) (rndMoons name) (sprintf "%s\n%s" name longDescLorem)

let rndPlanets starName =
    let mutable lis = []
    let nPlanets = rndInt 0 5

    for i in 1 .. nPlanets do
        let pName = sprintf "%s %s" starName (intToRoman i)
        lis <- List.append  [rndPlanet pName] lis
    lis

let rndSun = fun n ->
    let name = starNames.[rndInt 1 10]
    let planets = rndPlanets(name)
    let color = rndColor()
    { Name = name; Size = rndSize(); Planets = planets ; Color = color}
    

let generate =
    let mutable lis = []

    for i in 1 .. 5000 do
        let pos = rndPos()
        let sun = rndSun()
        lis <- List.append  [{ Position = pos; Sun = sun}] lis

    { Systems = lis }