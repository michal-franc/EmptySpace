using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    //TODO: Some nice sprite for this
    //TODO: position of this seleciton is dependand on star size
    public class StarSelectedIndicatorControl : IBaseControl
    {
        private Shape _shape;
        public FloatRect GlobalBounds => _shape.GetGlobalBounds();

        public StarSelectedIndicatorControl(Vector2f starPosition)
        {
            var r = 20.0f;
            var middle = starPosition - new Vector2f((r / 2) + 2, (r / 2) + 2);
            _shape = new CircleShape(r);
            _shape.Position = middle;
            _shape.OutlineColor = new Color(255, 0, 0, 150);
            _shape.FillColor = Color.Transparent;
            _shape.OutlineThickness = 5;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shape);
        }
    }
}