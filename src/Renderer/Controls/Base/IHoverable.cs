using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public interface IHoverable
    {
        event OnHoverHandler OnHover;
        void Hover(RenderTarget target);
        FloatRect GetGlobalBounds();
    }
}