module StorageTests

open Xunit
open FsUnit.Xunit
open Storage

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