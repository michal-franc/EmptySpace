using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public interface IBaseControl : Drawable
    {
        FloatRect GlobalBounds { get; }
    }
}