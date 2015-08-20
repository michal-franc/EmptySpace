using System.Collections.Generic;
using SFML.Window;

namespace Renderer.Controls.Base
{
    public interface IControlContainer : IBaseControl
    {
        IEnumerable<IBaseControl> ChildrenControls { get; }
        Vector2f Position { get; }
    }
}