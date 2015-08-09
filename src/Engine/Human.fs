module Human

type Action = Exploring | Nothing

[<Literal>]
let Rate = 1

let mutable counter = 0

type Human = {
   Id : int
   Name : string
   Hunger : int
   Thirst : int
   Health : int
   Tired : int
   Action : Action
} with
    member this.tick = 
        let tiredRate = match this.Action with
                        | Nothing -> 0
                        |  _ -> 1
        let tiredValue = this.Tired + tiredRate 

        { this with Hunger = this.Hunger + Rate ; Thirst = this.Thirst + Rate; Tired = if tiredValue < 0 then 0 else tiredValue }
    member this.getNeeds = 
            let mutable needs = []
            needs <- List.append needs (if this.Hunger > 30 then  [("Food", 1.0f)] else [])
            needs <- List.append needs (if this.Thirst > 30 then  [("Water", 1.0f)] else [])
            needs
    member this.satisfyNeeds satisfiedNeeds = 
            let mutable modifiedT = this
            for name, value in satisfiedNeeds do
                 modifiedT <- match name with 
                                 | "Food" -> { modifiedT with Hunger = modifiedT.Hunger - 30 }
                                 | "Water" -> { modifiedT with Thirst = modifiedT.Thirst - 30 }
                                 | _ -> modifiedT
            modifiedT
    member this.print =
            sprintf "%s hunger : %i thirst : %i tired : %i action: %s" this.Name this.Hunger this.Thirst this.Tired (match this.Action with
                                                                                                                    | Exploring-> "Explore"
                                                                                                                    | _ -> "None")
            
            

let create name = 
    counter <- counter + 1
    {
       Id = counter
       Name = name
       Hunger = 0
       Thirst = 0
       Tired = 0
       Health = 100
       Action = Nothing
    }   