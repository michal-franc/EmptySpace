using SFML.Graphics;
using SFML.Window;

namespace Renderer.CustomShapes
{
    public class StarShape : CircleShape
    {
        public StarShape(StarSystem.StarSystem system, Vector2f position)
            :base(2.0f)
        {
            this.Position = position;
            this.FillColor = Color.White;
            this.OutlineColor = system.Sun.Color;
            this.OutlineThickness = 3;
        }
    }
}