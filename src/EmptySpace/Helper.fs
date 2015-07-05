module Helper

open SFML.Window
open SFML.Graphics

let vector t = new Vector2f(fst t, snd t)
let vector1 t = new Vector2f(t, t)

let centerPos (bounds:FloatRect) =
    ( bounds.Left + (bounds.Width / 2.0f), (bounds.Height / 2.0f) + bounds.Top )

let addPos (pos:float32*float32) xy =
    (fst pos + fst xy, snd pos + snd xy)

let adjustPos pos (bounds:FloatRect) = 
   (fst pos - (bounds.Width / 2.0f), snd pos - (bounds.Height / 2.0f))