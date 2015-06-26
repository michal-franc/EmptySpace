module LocationView

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
        Console.WriteLine("\n\n\n\n\n")
        Console.WriteLine("s. scan planet\n")
        Console.WriteLine("\npress space to exit")

        let key = Console.ReadKey()
        match key.Key with 
                   | ConsoleKey.Spacebar -> location
                   | ConsoleKey.S -> location
                   | _ -> this.loop location

    interface IView with
        member this.innerLoop state = 
            let newLocation = this.loop state.Location
            { state with CurrentView = Menu; Location = newLocation }