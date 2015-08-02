using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public class PlayerIndicatorControl : IBaseControl
    {
        private Shape _shape;
        public FloatRect GlobalBounds => _shape.GetGlobalBounds();

        public PlayerIndicatorControl(Vector2f playerPosition)
        {
            _shape = new CircleShape(100.0f);
            _shape.Position = playerPosition;
            _shape.OutlineColor = Color.Yellow;
            _shape.FillColor = Color.Transparent;
            _shape.OutlineThickness = 20;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shape);
        }
    }
}