module Ship

open Storage
open SFML.Window
open CrewList
open Helper

type Ship = {
    Name : string
    CrewList : CrewList
    Storage : Storage
    Position : Vector2f
    Destination : Vector2f
    } with 
     member this.tick () =
        let (crewlist, newStorage) = this.CrewList.tick(this.Storage);
        
        //TODO: Move this whole logic to SHIP tick
        //TODO: BUG - Slight mismatch beetwen dest and position ( it wont reach 0.0f ) -> introduce either error rate or fix the positioning
        let speed = 0.5f;

        let mutable whereToGo = this.Position - this.Destination;
        if abs whereToGo.X < 0.3f && whereToGo.X <> 0.0f then
            whereToGo <- vector (0.0f, whereToGo.Y)

        if abs whereToGo.Y < 0.3f && whereToGo.Y <> 0.0f then
            whereToGo <- vector (whereToGo.X, 0.0f)

        let moveVector = (normalize whereToGo) * speed;

        if moveVector.X <> 0.0f || moveVector.Y <> 0.0f then
            let (newStorage, x) = Storage.take "Fuel" 0.01f this.Storage
            if x > 0.0f then
                { this with Storage = newStorage; Position = this.Position - moveVector }
            else
                { this with Storage = newStorage; }
        else
            this

let createDefault startPos =
    { 
        Name = "TestShip";
        Position = startPos;
        Destination = startPos;
        Storage = Storage.createDefault;
        CrewList = CrewList.createDefault;
    }