using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    public static class ShapeHelper
    {
        public static Shape RectangleWithColor(Vector2f size, Color color)
        {
            var rect = new RectangleShape(size);
            rect.FillColor = color;
            return rect;
        }

        public static Shape Rectangle(Vector2f vector2F, Vector2f pos, Color color)
        {
            var rect = new RectangleShape(vector2F);
            rect.FillColor = color;
            rect.Position = pos;
            return rect;
        }

        public static Shape Rectangle(Vector2f vector2F, Vector2f pos)
        {
            var rect = new RectangleShape(vector2F);
            rect.Position = pos;
            return rect;
        }
    }

    public class TopBar : IBaseControl
    {
        public FloatRect GlobalBounds => _rect.GetGlobalBounds();

        private readonly string _text;
        private readonly Shape _rect;

        public TopBar(string text, float size)
        {
            _rect = ShapeHelper.RectangleWithColor(new Vector2f(size, 30.0f), Color.Yellow);
            _text = text;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);

            var h = new Text(_text, GlobalAssets.FontBold, 15);
            var pos = Helper.centerPos(this._rect.GetGlobalBounds());
            var adjustedPos = Helper.adjustPos(pos.Item1, pos.Item2, h.GetGlobalBounds());
            h.Position = new Vector2f(adjustedPos.Item1, adjustedPos.Item2);
            h.Color = Color.Black;
            target.Draw(h);
        }
    }
}
