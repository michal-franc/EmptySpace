using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public interface IClickable : IBaseControl
    {
        event OnClickHandler OnClick;
        GameState Click(RenderTarget target, GameState state);
    }
}