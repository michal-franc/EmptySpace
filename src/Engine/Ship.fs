module Ship

open Storage
open SFML.Window
open CrewList
open VectorHelper

let private isCloseEnough (v:Vector2f) =
    abs v.X < 0.3f && abs v.Y < 0.3f

let private pathToDest pos dest =
     let whereToGo =  pos - dest;

     if whereToGo |> isCloseEnough then
        vector0()
     else 
        whereToGo |> normalize

type Ship = {
    Name : string
    CrewList : CrewList
    Storage : Storage
    Position : Vector2f
    Destination : Option<Vector2f * int>
    } with 
     member this.move(ship, dest) = 
        let speed = 0.5f;
        let moveVector = (pathToDest ship.Position dest) * speed;

        if moveVector |> isZero then
             { ship with Destination = None }
        else
            let (newStor, x) = Storage.take "Fuel" 0.01f ship.Storage
            { ship with Position = ship.Position - moveVector; Storage = newStor }

     member this.tick () =
        let (newCrewList, newStorage) = this.CrewList.tick(this.Storage);
        let newShip = { this with Storage = newStorage; CrewList = newCrewList}
        
        match this.Destination with
        | Some s -> this.move(newShip, fst s)
        | None -> newShip

let createDefault startPos =
    { 
        Name = "TestShip";
        Position = startPos;
        Destination = None;
        Storage = Storage.createDefault;
        CrewList = CrewList.createDefault;
    }