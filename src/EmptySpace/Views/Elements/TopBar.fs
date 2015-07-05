module TopBar
open SFML.Graphics
open SFML.Window
open Helper

type TopBar(text, wndSize, font) =
    let wholeLength = vector (wndSize, 30.0f)
    let font = font
    let text = text

    interface  Drawable with
        member this.Draw(target, renderState) = 
               let mutable rect = new RectangleShape(wholeLength)
               rect.FillColor <- Color.Yellow
               target.Draw(rect)

               let mutable t = new Text(text, font)
               t.Scale <- vector1 (0.5f)
               let pos = centerPos (rect.GetGlobalBounds())
               t.Position <- adjustPos pos (t.GetGlobalBounds()) |> vector
               t.Color <- Color.Black
               target.Draw(t)