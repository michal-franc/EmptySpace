module Player

    open SFML.Window

    type T = { TileId:int ; Position:Vector2u }

    let create tileId position =
        { TileId = tileId; Position = position }