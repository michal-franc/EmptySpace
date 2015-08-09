module Ship

open Storage
open SFML.Window
open CrewList
open Helper

let private isCloseEnough (v:Vector2f) =
    abs v.X < 0.3f && v.Y < 0.3f

let private pathToDest pos dest =
     let whereToGo =  pos - dest;

     if whereToGo |> isCloseEnough then
        vector0()
     else 
        whereToGo

type Ship = {
    Name : string
    CrewList : CrewList
    Storage : Storage
    Position : Vector2f
    Destination : Vector2f
    } with 
     member this.tick () =
        let (newCrewList, newStorage) = this.CrewList.tick(this.Storage);
        let newShip = { this with Storage = newStorage; CrewList = newCrewList}
        let speed = 0.5f;

        let moveVector = ((pathToDest this.Position this.Destination) |> normalize) * speed;

        if moveVector |> isZero then
            newShip
        else
            let (newStor, x) = Storage.take "Fuel" 0.01f newStorage
            if x > 0.0f then
                { newShip with Position = this.Position - moveVector; Storage = newStor }
            else
                newShip

let createDefault startPos =
    { 
        Name = "TestShip";
        Position = startPos;
        Destination = startPos;
        Storage = Storage.createDefault;
        CrewList = CrewList.createDefault;
    }