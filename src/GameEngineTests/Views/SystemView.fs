module SystemView

open IView
open System
open ViewType
open GameState
open libtcod
open TopBar

type ObjectType = Sun | Planet | Moon | Asteroid
type Size = Huge | Large | Medium | Small | Tiny
type PlanetType = Barren | Swamp | Earthlike | Gas

type Sun = {
    Size : Size
    Color : TCODColor
}

type Moon = {
    Size : Size
    Type : PlanetType
}

type Planet = {
    Size : Size
    Type : PlanetType
    Moons : Moon list
}

type StarSystem = {
   Sun : Sun
   Planets : Planet list
}

type SystemView() =
    
    let translateSize objectType size =
        let multiplier = match objectType with
                         | Sun -> 2.0
                         | Planet -> 1.0
                         | Moon -> 0.5
                         | Asteroid -> 0.3

        let size = match size with
                   | Huge -> 10.0
                   | Large -> 7.0
                   | Medium -> 5.0
                   | Small -> 2.0
                   | Tiny -> 1.5

        size * multiplier
        

    let test = {
        Sun = {
         Size = Huge;
         Color = TCODColor.red

        }
        Planets = [{ 
                    Size = Large; 
                    Type = Barren; 
                    Moons = [
                        { Size = Medium; Type = Swamp };
                        { Size = Small; Type = Gas};
                        { Size = Small; Type = Earthlike};
                        { Size = Small; Type = Earthlike};
                        { Size = Small; Type = Earthlike};
                        { Size = Small; Type = Earthlike};

                    ]};
                    {
                    Size = Large; 
                    Type = Barren; 
                    Moons = [
                        { Size = Medium; Type = Swamp };
                        { Size = Small; Type = Gas};
                        { Size = Small; Type = Earthlike}
                    ]};
                    {
                    Size = Medium; 
                    Type = Barren; 
                    Moons = [
                        { Size = Medium; Type = Swamp };
                        { Size = Small; Type = Gas};
                        { Size = Small; Type = Earthlike}
                    ]};
       ] 
    }

    member this.DrawSun (sun:Sun) r = 
        TCODConsole.root.setBackgroundColor(sun.Color);
        TCODConsole.root.rect(0, 9, r, r, true, TCODBackgroundFlag.Set);
        TCODConsole.root.setBackgroundColor(TCODColor.black);


    member this.DrawMoons (moons:Moon list) rPlanet yPlanet xPlanet =

        let distance = 3
        let x = xPlanet - rPlanet
        let mutable initialShift = yPlanet + 6

        for m in moons do
            let rMoon = int(translateSize Moon m.Size)
            TCODConsole.root.setBackgroundColor(TCODColor.white);
            TCODConsole.root.rect(x, initialShift + distance, rMoon, rMoon, true, TCODBackgroundFlag.Set);
            TCODConsole.root.setBackgroundColor(TCODColor.black);
            initialShift <- (initialShift + (rMoon * 2))

    member this.DrawPlanets (planets:Planet list) shift =
        
        let distance = 5
        let y = 15

        let mutable initialShift = shift

        for p in planets do
            let rPlanet = int(translateSize Planet p.Size)
            TCODConsole.root.setBackgroundColor(TCODColor.blue);
            TCODConsole.root.rect(initialShift + distance, y, rPlanet, rPlanet, true, TCODBackgroundFlag.Set);
            TCODConsole.root.setBackgroundColor(TCODColor.black);
            initialShift <- (initialShift + (rPlanet * 2))
            this.DrawMoons p.Moons rPlanet y initialShift


    member this.DrawView location = 

        let rSun = int(translateSize Sun test.Sun.Size |> Math.Floor)

        this.DrawSun test.Sun rSun
        this.DrawPlanets test.Planets rSun

        let img = new TCODImage("planets.png");
        img.blit(TCODConsole.root, 0.0f, 25.0f, TCODBackgroundFlag.Set, 0.3f, 0.3f);


        topBarText (sprintf "System - %s (16874,-549)" location.Name)
    
    member this.loop state = 
        TCODConsole.root.clear()
        this.DrawView state.Location
        TCODConsole.flush()

        this.loop state

    interface IView with
        member this.innerLoop state = 
            this.loop state
            { state with CurrentView = Menu; }