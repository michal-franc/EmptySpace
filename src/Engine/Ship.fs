module Ship

open Storage
open SFML.Window
open CrewList
open VectorHelper
open Destination

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
    Destination : Destnation
    } with 
     member this.move(ship, pos, id) = 
        let speed = 0.5f;
        let moveVector = (pathToDest ship.Position pos) * speed;

        if moveVector |> isZero then
             { ship with Destination = Arrived(id) }
        else
            let (newStor, x) = Storage.take "Fuel" 0.01f ship.Storage
            { ship with Position = ship.Position - moveVector; Storage = newStor }

     member this.tick (state) =
        let (newCrewList, newStorage) = this.CrewList.tick(this.Storage);
        let newShip = { this with Storage = newStorage; CrewList = newCrewList}
        
        match this.Destination with
        | OnRoute (id, pos) -> this.move(newShip, pos, id)
        | Arrived id -> { newShip with Destination = None } 
        | None -> newShip

let createDefault startPos =
    { 
        Name = "TestShip";
        Position = startPos;
        Destination = None;
        Storage = Storage.createDefault;
        CrewList = CrewList.createDefault;
    }