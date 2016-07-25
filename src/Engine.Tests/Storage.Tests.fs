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
    let sut = create (Map.ofList[("a", 1.0f);("b", 2.0f)])
    sut.Objects.Count |> should equal 2

[<Fact>] 
let ``I can get number of items i have requested for if there are enough of them`` ()=
    let sut = create (Map.ofList[("a", 1.0f)])
    let newSut, num = take "a" 1.0f sut
    num |> should equal 1.0f
    newSut.Objects.["a"] |> should equal 0.0f

[<Fact>]
let ``Items taken from the storage are substracted`` ()=
    let sut = create (Map.ofList[("a", 1.0f)])
    let newSut, num = take "a" 1.0f sut
    num |> should equal 1.0f
    newSut.Objects.["a"] |> should equal 0.0f

[<Fact>]
let ``If there are not enough items then return the number that is possible to get`` ()=
    let sut = create (Map.ofList[("a", 1.0f)])
    let newSut, num = take "a" 2.0f sut
    num |> should equal 1.0f
    newSut.Objects.["a"] |> should equal 0.0f

[<Fact>] 
let ``If there are no items i get back 0`` ()=
    let sut = create (Map.ofList[("a", 1.0f);("b", 2.0f)])
    let newSut, num = take "c" 1.0f sut
    num |> should equal 0.0f

[<Fact>]
let ``I can use update function to add new objects`` ()=
    let sut = createEmpty
    let updatesList = Map.ofList[("a", 1.0f);("b", 2.0f)]

    sut.Objects.Count |> should equal 0

    let newSut = addItems updatesList sut
    newSut.Objects.Count |> should equal 2
    newSut.Objects.["a"] |> should equal 1.0f

[<Fact>]
let ``I can use update function to update existing objects`` ()=
    let updatesList = Map.ofList[("a", 1.0f);("b", 2.0f)]
    let sut = createEmpty |> addItems updatesList

    (Map.find "a" sut.Objects) |> should equal 1.0f

    let newUpdate = Map.ofList[("a", 2.0f);("b", -1.0f);("c", 1.0f)]
    let newSut = addItems newUpdate sut
    (Map.find "a" newSut.Objects) |> should equal 3.0f
    (Map.find "b" newSut.Objects) |> should equal 1.0f
    (Map.find "c" newSut.Objects) |> should equal 1.0f
    newSut.Objects.Count |> should equal 3

[<Fact>]
let ``I can an item from the list`` ()=
    let updatesList = Map.ofList[("a", 1.0f);("b", 2.0f)]
    let sut = createEmpty |> addItems updatesList

    (Map.find "a" sut.Objects) |> should equal 1.0f

    let newUpdate = Map.ofList[("a", 1.0f);("b", 1.0f)]
    let newSut = removeItems newUpdate sut
    (Map.find "a" newSut.Objects) |> should equal 0.0f
    (Map.find "b" newSut.Objects) |> should equal 1.0f
    newSut.Objects.Count |> should equal 2


[<Fact>]
let ``For given list of items, return those that are in the storage`` ()=
    let updatesList = Map.ofList[("a", 1.0f);("b", 2.0f)]
    let sut = createEmpty |> addItems updatesList

    sut.checkItems ["a", 1.0f; "b", 1.0f; "c", 1.0f] |> should equal ["a", 1.0f;"b", 1.0f]