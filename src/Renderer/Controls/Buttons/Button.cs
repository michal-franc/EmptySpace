using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public interface IClickable
    {
        event OnClickHandler OnClick;
    }

    public interface IHoverable
    {
        event OnHoverHandler OnHover;
    }


    public class Button : ControlShapeBase
    {
        private readonly string _text;
        private readonly Vector2f _pos;

        public Button(string text, Vector2f pos, string hint = "")
            :base(pos, ShapeHelper.Rectangle(new Vector2f(40.0f, 20.0f), pos), hint)
        {
            _text = text;
            _pos = pos;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            var h = new Text(_text, GlobalAssets.Font);
            h.Scale = new Vector2f(0.4f, 0.4f);
            h.Position = new Vector2f(_pos.X, _pos.Y);
            h.Color = Color.Black;
            target.Draw(h);
        }
    }
}
