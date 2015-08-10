module Human

type Action = Exploring | Nothing

[<Literal>]
let Rate = 0.01f

let mutable counter = 0

type Human = {
   Id : int
   Name : string
   Hunger : float32
   Thirst : float32
   Health : float32
   Tired : float32
   Action : Action
} with
    member this.tick = 
        let tiredRate = match this.Action with
                        | Nothing -> 0.0f
                        |  _ -> 0.01f
        let tiredValue = this.Tired + tiredRate 

        { this with Hunger = this.Hunger + Rate ; Thirst = this.Thirst + Rate; Tired = if tiredValue < 0.0f then 0.0f else tiredValue }
    member this.getNeeds = 
            let mutable needs = []
            needs <- List.append needs (if this.Hunger > 30.0f then  [("Food", 1.f)] else [])
            needs <- List.append needs (if this.Thirst > 30.0f then  [("Water", 1.f)] else [])
            needs
    member this.satisfyNeeds satisfiedNeeds = 
            let mutable modifiedT = this
            for name, value in satisfiedNeeds do
                 modifiedT <- match name with 
                                 | "Food" -> { modifiedT with Hunger = modifiedT.Hunger - 30.0f }
                                 | "Water" -> { modifiedT with Thirst = modifiedT.Thirst - 30.0f }
                                 | _ -> modifiedT
            modifiedT
    member this.print =
            sprintf "%s hunger : %.0f thirst : %.0f tired : %.0f action: %s" this.Name this.Hunger this.Thirst this.Tired (match this.Action with
                                                                                                                    | Exploring-> "Explore"
                                                                                                                    | _ -> "None")
            
            

let create name = 
    counter <- counter + 1
    {
       Id = counter
       Name = name
       Hunger = 0.0f
       Thirst = 0.0f
       Tired = 0.0f
       Health = 100.0f
       Action = Nothing
    }   