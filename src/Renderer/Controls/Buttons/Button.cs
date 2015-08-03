using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    // TODO: Create a helper to generate a rect with text inside, currently i have to do this manualy
    public class Button : Clickable
    {
        private readonly string _text;
        private readonly Vector2f _pos;
        private Shape _rect;

        public override FloatRect GlobalBounds => _rect.GetGlobalBounds();

        public Button(string text, Vector2f pos, IControlContainer parent = null)
        {
            _text = text;
            if (parent != null) { _pos = pos + parent.Position; }
            else _pos = pos;
            _rect = ShapeHelper.Rectangle(new Vector2f(40.0f, 20.0f), _pos);
            _rect.FillColor = Color.White;
        }

        public FloatRect GetGlobalBounds()
        {
            return _rect.GetGlobalBounds();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);

            var h = new Text(_text, GlobalAssets.FontBold);
            h.Scale = new Vector2f(0.4f, 0.4f);
            h.Position = new Vector2f(_pos.X, _pos.Y);
            h.Color = Color.Black;
            target.Draw(h);
        }

    }
}
