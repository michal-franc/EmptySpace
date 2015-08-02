using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public interface IClickable
    {
        event OnClickHandler OnClick;
        GameState Click(RenderTarget target, GameState state);
        FloatRect GetGlobalBounds();
    }
}