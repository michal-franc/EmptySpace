module Storage

open System.Collections
open Xunit
open FsUnit.Xunit

type Storage = {
    Objects : Map<string,int> 
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
        | _ -> storage.Objects |> Map.fold(fun state key item -> state + (sprintf "%s - %i" key item) + "\n") ""

let createEmpty = 
    {
        Objects = Map.empty
    }

let createDefault = 
    {
        Objects = (Map.ofList[("Food", 100);("Water", 100)])
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
        let updateVal = if currentVal - num  < 0 then -currentVal else -num 
        addItems (Map.ofList[(itemName, updateVal)]) storage 

let take itemName howMany storage =
    match Map.tryFind itemName storage.Objects with
        | Some value' -> ( takeAndSubstract itemName storage howMany value', value' )
        | None -> (storage, 0)


[<Fact>] 
let ``Newly created storage is empty`` ()=
    let sut = createEmpty
    sut.Objects.Count |> should equal 0
    
[<Fact>] 
let ``I can create storage with predefined objects`` ()=
    let sut = create (Map.ofList[("a", 1);("b", 2)])
    sut.Objects.Count |> should equal 2

[<Fact>] 
let ``I can get number of items i have requested for if there are enough of them`` ()=
    let sut = create (Map.ofList[("a", 1)])
    let newSut, num = take "a" 1 sut
    num |> should equal 1
    newSut.Objects.["a"] |> should equal 0

[<Fact>]
let ``Items taken from the storage are substracted`` ()=
    let sut = create (Map.ofList[("a", 1)])
    let newSut, num = take "a" 1 sut
    num |> should equal 1
    newSut.Objects.["a"] |> should equal 0

[<Fact>]
let ``If there are not enough items then return the number that is possible to get`` ()=
    let sut = create (Map.ofList[("a", 1)])
    let newSut, num = take "a" 2 sut
    num |> should equal 1
    newSut.Objects.["a"] |> should equal 0

[<Fact>] 
let ``If there are no items i get back 0`` ()=
    let sut = create (Map.ofList[("a", 1);("b", 2)])
    let newSut, num = take "c" 1 sut
    num |> should equal 0

[<Fact>]
let ``I can use update function to add new objects`` ()=
    let sut = createEmpty
    let updatesList = Map.ofList[("a", 1);("b", 2)]

    sut.Objects.Count |> should equal 0

    let newSut = addItems updatesList sut
    newSut.Objects.Count |> should equal 2
    newSut.Objects.["a"] |> should equal 1

[<Fact>]
let ``I can use update function to update existing objects`` ()=
    let updatesList = Map.ofList[("a", 1);("b", 2)]
    let sut = createEmpty |> addItems updatesList

    (Map.find "a" sut.Objects) |> should equal 1

    let newUpdate = Map.ofList[("a", 2);("b", -1);("c", 1)]
    let newSut = addItems newUpdate sut
    (Map.find "a" newSut.Objects) |> should equal 3
    (Map.find "b" newSut.Objects) |> should equal 1
    (Map.find "c" newSut.Objects) |> should equal 1
    newSut.Objects.Count |> should equal 3

[<Fact>]
let ``I can an item from the list`` ()=
    let updatesList = Map.ofList[("a", 1);("b", 2)]
    let sut = createEmpty |> addItems updatesList

    (Map.find "a" sut.Objects) |> should equal 1

    let newUpdate = Map.ofList[("a", 1);("b", 1)]
    let newSut = removeItems newUpdate sut
    (Map.find "a" newSut.Objects) |> should equal 0
    (Map.find "b" newSut.Objects) |> should equal 1
    newSut.Objects.Count |> should equal 2


[<Fact>]
let ``For given list of items, return those that are in the storage`` ()=
    let updatesList = Map.ofList[("a", 1);("b", 2)]
    let sut = createEmpty |> addItems updatesList

    sut.checkItems ["a", 1; "b", 1; "c", 1] |> should equal ["a", 1;"b", 1]