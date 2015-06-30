module SystemView

open IView
open System
open ViewType
open GameState

type SystemView() =

    member this.GetView location = 
        sprintf "%A %A %s" location.Size location.Type location.Name
    
    member this.loop state = 

        Console.Clear()
        Console.Write(this.GetView state.Location )
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
                   | ConsoleKey.Spacebar -> state.Location 
                   | ConsoleKey.D1 -> state.Location
                   | ConsoleKey.D2 -> state.Location
                   | ConsoleKey.D3 -> state.Location
                   | ConsoleKey.D4 -> state.Location
                   | _ -> this.loop state

    interface IView with
        member this.innerLoop state = 
            let newLocation = this.loop state
            { state with CurrentView = Menu; Location = newLocation }