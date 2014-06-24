module World

    open SFML.Window

    type T = { Tiles : int[,] }

    let spaceTileId = 3

    let create (width : uint32) (heigth : uint32) = 
        let howMany = (int (width * heigth * 10ul))
        let spaceTiles = Array2D.init (int width) (int heigth) (fun x y -> ( spaceTileId))
        let spaceShip =
         [|
            250;64;64;64
            250;64;64;64
            250;64;64;64
            64;64;64;64
            64;64;64;64
            64;64;64;64
         |]

        { Tiles = spaceTiles }