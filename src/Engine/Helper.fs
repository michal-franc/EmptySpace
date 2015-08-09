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

let normalize (t:Vector2f) =
    let distance = sqrt (t.X * t.X + t.Y * t.Y);
    if distance > 0.0f then
        vector (t.X / distance, t.Y / distance)
    else
        vector1 0.0f