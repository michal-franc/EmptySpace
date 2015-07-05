module SpriteWithHint
open SFML.Graphics
open SFML.Window
open Helper

 type SpriteWithHint(hint, texture, intRect, font) =
    inherit Sprite(texture, intRect) with
    let hint = hint
    let font = font

    member this.DrawHint(window:RenderWindow) =
        let mPos = Mouse.GetPosition(window)
        if this.MouseInSprite(mPos) then
            let t, b = this.HintTextRect()
            window.Draw(b)
            window.Draw(t)
        

    member this.MouseInSprite(vector:Vector2i) =
        // cant belevie that it works from the start without any bugs :D
        // this is a little (hack) but it kinda works
        // create a little rect based on mouse pos and then just do a intersect
        // BUG: zoom out ( change in view ) causes this to fail ;)
        let test = new FloatRect(float32(vector.X), float32(vector.Y), 1.0f, 1.0f)
        this.GetGlobalBounds().Intersects(test)

    member this.HintTextRect() =
        let mutable back = new RectangleShape(vector (120.0f, 20.0f))
        back.Position <-  addPos (20.0f, 0.0f) (this.Position.X, this.Position.Y) |> vector
        back.FillColor <- Color.Yellow

        let h = new Text(hint, font)
        h.Scale <- vector1 (0.4f)
        let pos = centerPos (back.GetGlobalBounds())
        h.Position <- adjustPos pos (h.GetGlobalBounds()) |> vector
        h.Color <- Color.Black

        (h, back)

