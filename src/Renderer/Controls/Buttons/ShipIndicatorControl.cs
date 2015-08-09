using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public class ShipIndicatorControl : IBaseControl, IUpdatable
    {
        private CircleShape _shape;
        public FloatRect GlobalBounds => _shape.GetGlobalBounds();

        public ShipIndicatorControl(Vector2f playerPosition)
        {
            var r = 5.0f;
            var middle = playerPosition - new Vector2f(r, r);
            _shape = new CircleShape(r);
            _shape.Position = middle;
            _shape.OutlineColor = new Color(255, 255, 0, 150);
            _shape.FillColor = Color.Red;
            _shape.OutlineThickness = 5;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shape);
        }

        public void Update(GameEngine.GameState state)
        {
            var r = _shape.Radius;
            var middle = state.Ship.Position - new Vector2f(r, r);
            _shape.Position = middle;
        }
    }
}