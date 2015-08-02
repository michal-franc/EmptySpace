using System;
using SFML.Graphics;

namespace Renderer.Controls
{
    //TODO: remove event args, hover can manipulate the view only
    public delegate void OnHoverHandler(RenderTarget sender, EventArgs e);
}