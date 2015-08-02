using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Base
{
    public interface IBaseControl : Drawable
    {
    }

    public interface IControlContainer : IBaseControl
    {
        IEnumerable<IBaseControl> ChildrenControls { get; }
        Vector2f Position { get; }
        FloatRect GlobalBounds { get; }
    }
}