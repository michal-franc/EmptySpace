module SpriteWithHint
open SFML.Graphics
open SFML.Window
open Helper

 type SpriteWithHint(hint, texture, intRect, font) =
    inherit Sprite(texture, intRect) with
    let hint = hint
    let font = font

    member this.DrawHint(window:RenderWindow) =
        let t = this.HintTextRect()
        window.Draw(t)

    member this.DrawSelected(window:RenderWindow) =
        let c = this.SelectRect()
        window.Draw(c)
        this.DrawHint(window)

    member this.MouseInSprite(vector:Vector2i) =
        // cant belevie that it works from the start without any bugs :D
        // this is a little (hack) but it kinda works
        // create a little rect based on mouse pos and then just do a intersect
        // BUG: zoom out ( change in view ) causes this to fail ;)
        let test = new FloatRect(float32(vector.X), float32(vector.Y), 1.0f, 1.0f)
        this.GetGlobalBounds().Intersects(test)

    
    member this.SelectRect() =
        let mutable circle = new CircleShape(5.0f)
        let pos = centerPos (this.GetGlobalBounds()) 
        circle.Position <- adjustPos pos (circle.GetGlobalBounds())  |> vector
        circle.FillColor <- Color.Red
        circle
        

    member this.HintTextRect() =
        let h = new Text(hint, font)
        h.Scale <- vector1 (0.4f)
        h.Position <- addPos (0.0f, -20.0f) (this.Position.X, this.Position.Y) |> vector
        h.Color <- Color.Yellow
        h

