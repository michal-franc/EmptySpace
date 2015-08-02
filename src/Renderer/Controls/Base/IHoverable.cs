using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public interface IHoverable : IBaseControl
    {
        event OnHoverHandler OnHover;
        void Hover(RenderTarget target);
    }
}