module World

    open SFML.Window

    type T = { Tiles : int[,] }

    let spaceShipTiles = array2D [
        [ 420;64;64;64 ]
        [ 419;64;64;64 ]
        [ 419;64;64;64 ]
        [ 419;64;64;64 ]
        [ 64;64;64;64 ]
        [ 64;64;64;64 ]
     ]

    let spaceShip1 = WorldObject.create spaceShipTiles (20, 20)
    let spaceShip2 = WorldObject.create spaceShipTiles (5, 5)
    let spaceShip3 = WorldObject.create spaceShipTiles (30, 30)

    let objects = [ spaceShip1;spaceShip2;spaceShip3 ]

    let spaceTileId = 3

    let addObject (target: int[,]) (source: int[,]) (x, y) =
        Array2D.blit source 0 0 target x y 5 4

    let create (width : uint32) (heigth : uint32) = 
        let howMany = (int (width * heigth * 10ul))
        let spaceTiles = Array2D.init (int width) (int heigth) (fun x y -> ( spaceTileId))

        objects |> List.iter (fun x -> addObject spaceTiles x.Tiles x.Position)
        
        { Tiles = spaceTiles }