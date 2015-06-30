﻿module LocationView

open IView
open System
open ViewType
open GameState

type LocationView() =

    member this.GetView location = 
        sprintf "%A %A %s" location.Size location.Type location.Name
    
    member this.loop location = 

        Console.Clear()
        Console.Write(this.GetView location)
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("1. Sun - Red Dwarf XF-4")
        Console.WriteLine("2. Planet LX-717")
        Console.WriteLine("3. Asteroid Field")
        Console.WriteLine("4. Asteroid Field")
        Console.WriteLine("5. Planet LX-817")
        Console.WriteLine("")
        Console.WriteLine("?? there are still unknown locations")
        Console.WriteLine("s. Explore System")
        Console.WriteLine("\npress space to exit")

        let key = Console.ReadKey()
        match key.Key with 
                   | ConsoleKey.Spacebar -> location
                   | ConsoleKey.NumPad0 -> location
                   | _ -> this.loop location

    interface IView with
        member this.innerLoop state = 
            let newLocation = this.loop state.Location
            { state with CurrentView = Menu; Location = newLocation }