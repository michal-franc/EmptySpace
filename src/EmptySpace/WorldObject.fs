module WorldObject

    open SFML.Window

    type T = { Tiles:int[,] ; Position: int * int }

    let create tiles (x, y) =
        { Tiles = tiles; Position = (x, y) }