module Storage

open System.Collections
open Xunit
open FsUnit.Xunit

type Storage = {
    Objects : Map<string,int> 
}

let private mergeSum map1 map2 =
    Map.fold (fun map key value ->
        match Map.tryFind key map with
        | Some value' -> Map.add key (value + value') map
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

let update updateList oldStorage:Storage =
    {
        Objects = mergeSum oldStorage.Objects updateList
    }

let get itemName storage =
    match Map.tryFind itemName storage.Objects with
        | Some value' -> value'
        | None -> 0

[<Fact>] 
let ``Newly created storage is empty`` ()=
    let sut = createEmpty
    sut.Objects.Count |> should equal 0
    
[<Fact>] 
let ``I can create storage with predefined objects`` ()=
    let sut = create (Map.ofList[("a", 1);("b", 2)])
    sut.Objects.Count |> should equal 2

[<Fact>] 
let ``I can query the storage and get value`` ()=
    let sut = create (Map.ofList[("a", 1);("b", 2)])
    let obj = get "a" sut
    obj |> should equal 1

[<Fact>] 
let ``I can query the storage and if there is no value return 0`` ()=
    let sut = create (Map.ofList[("a", 1);("b", 2)])
    let obj = get "c" sut
    obj |> should equal 0

[<Fact>]
let ``I can use update function to add new objects`` ()=
    let sut = createEmpty
    let updatesList = Map.ofList[("a", 1);("b", 2)]

    sut.Objects.Count |> should equal 0

    let newSut = update updatesList sut
    newSut.Objects.Count |> should equal 2

[<Fact>]
let ``I can use update function to update existing objects`` ()=
    let updatesList = Map.ofList[("a", 1);("b", 2)]
    let sut = createEmpty |> update updatesList

    (Map.find "a" sut.Objects) |> should equal 1

    let newUpdate = Map.ofList[("a", 2);("b", -1);("c", 1)]
    let newSut = update newUpdate sut
    (Map.find "a" newSut.Objects) |> should equal 3
    (Map.find "b" newSut.Objects) |> should equal 1
    (Map.find "c" newSut.Objects) |> should equal 1
    newSut.Objects.Count |> should equal 3
