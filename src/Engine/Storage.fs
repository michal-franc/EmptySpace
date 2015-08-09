module Storage

open System.Collections


type Storage = {
    Objects : Map<string, float32> 
} with 
    member this.checkItems items = 
        let mutable list = []
        for item, amount in items do
            match Map.tryFind item this.Objects with
            | Some value -> list <- List.append list [item, amount]
            | None -> list <- List.append list []
        list

let private merge op map1 map2 =
    Map.fold (fun map key value ->
        match Map.tryFind key map with
        | Some value' -> Map.add key (op value' value) map
        | None -> Map.add key value map) map1 map2

let print storage =
    match storage.Objects.Count with
        | 0 -> "Storage is empty"
        | _ -> storage.Objects |> Map.fold(fun state key item -> state + (sprintf "%s - %.2f" key item) + "\n") ""

let createEmpty = 
    {
        Objects = Map.empty
    }

let createDefault = 
    {
        Objects = (Map.ofList[("Food", 100.0f);("Water", 100.0f);("Fuel", 1000.0f)])
    }

let create initialMap = 
    {
        Objects = initialMap
    }

let addItems updateList oldStorage:Storage =
    {
        Objects = merge (+) oldStorage.Objects updateList
    }

let removeItems updateList oldStorage:Storage = 
    {
        Objects = merge (-) oldStorage.Objects updateList
    }

let private takeAndSubstract itemName storage num currentVal=
        let updateVal = if currentVal - num  < 0.0f then -currentVal else -num 
        addItems (Map.ofList[(itemName, updateVal)]) storage 

let take itemName howMany storage =
    match Map.tryFind itemName storage.Objects with
        | Some value' -> ( takeAndSubstract itemName storage howMany value', value' )
        | None -> (storage, 0.0f)