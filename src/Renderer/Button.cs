using System;
using SFML.Graphics;
using SFML.Window;

namespace Renderer
{
    class Button : Drawable
    {
        private readonly string _text;
        private readonly Vector2f _pos;
        private readonly Font _font;
        private Action<GameState> _action;
        private RectangleShape _shape;

        public Button(string text, Vector2f pos, Font font, Action<GameState> action)
        {
            _text = text;
            _pos = pos;
            _font = font;
            _action = action;
            _shape = new RectangleShape(new Vector2f(40.0f, 20.0f));
            _shape.Position = _pos;
            _shape.FillColor = Color.Green;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_shape);

            var h = new Text(_text, _font);
            h.Scale = new Vector2f(0.4f, 0.4f);
            h.Position = new Vector2f(_pos.X, _pos.Y);
            h.Color = Color.Black;
            target.Draw(h);
        }

        public void HandleClick(Vector2i mPos, GameState state)
        {
            var test = new FloatRect(mPos.X, mPos.Y, 1.0f, 1.0f);
            if (_shape.GetGlobalBounds().Intersects(test))
            {
                _action(state);
            }
        }
    }
}
