using Renderer.StateEvents;
using Renderer.Views;
using SFML.Graphics;

namespace Renderer.Controls
{
    //TODO: Remove RenderTarget, click can only change state and cant influence the screen directly
    public delegate IViewStateChangeEvent OnClickHandler(RenderTarget sender, ViewState state);
}