using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace Renderer
{
    public class TopBar : Drawable
    {
        private readonly string _text;
        private readonly Font _font;
        private Vector2f _wholeLength;

        public TopBar(string text, float size, Font font)
        {
            _wholeLength = new Vector2f(size, 30.0f);
            _text = text;
            _font = font;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var rect = new RectangleShape(_wholeLength);
            rect.FillColor = Color.Yellow;
            target.Draw(rect);

            var h = new Text(_text, _font);
            h.Scale = Helper.vector1(0.5f);
            var pos = Helper.centerPos(rect.GetGlobalBounds());
            var adjustedPos = Helper.adjustPos(pos.Item1, pos.Item2, h.GetGlobalBounds());
            h.Position = new Vector2f(adjustedPos.Item1, adjustedPos.Item2);
            h.Color = Color.Black;
            target.Draw(h);
        }
    }
}
