module CrewList

open Human

type CrewList = {
    Crew : Human list 
 }

let createDefault = 
    {
        Crew = [ Human.create "Steve"; Human.create "Arnold" ]
    }

//let generateNeeds crewList = 

let print crewList =
    let s = crewList.Crew |> List.fold (fun previous crew -> previous + sprintf "%s" crew.Name + "\n") ""
    sprintf "%s" s