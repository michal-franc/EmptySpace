using SFML.Graphics;

namespace Renderer.Controls
{
    //TODO: Remove RenderTarget, click can only change state and cant influence the screen directly
    public delegate GameState OnClickHandler(RenderTarget sender, GameState state);
}