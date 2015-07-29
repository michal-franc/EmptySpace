using Renderer.CustomShapes;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public class StarControl : ControlShapeBase
    {
        public StarControl(StarSystem.StarSystem system)
            : base(new Vector2f(system.Position.X, system.Position.Y), 
                new StarShape(system, new Vector2f(system.Position.X, system.Position.Y)), 
                system.Sun.Name)
        {
        }
    }
}