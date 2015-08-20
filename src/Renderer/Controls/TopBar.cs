using System.Collections.Generic;
using System.Linq;
using Renderer.Controls.Base;
using Renderer.Controls.Buttons;
using Renderer.Resources;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    public static class ShapeHelper
    {
        public static Shape RectangleWithColor(Vector2f size, Color color)
        {
            var rect = new RectangleShape(size) {FillColor = color};
            return rect;
        }

        public static Shape Rectangle(Vector2f vector2F, Vector2f pos, Color color)
        {
            var rect = new RectangleShape(vector2F)
            {
                FillColor = color,
                Position = pos
            };
            return rect;
        }

        public static Shape Rectangle(Vector2f vector2F, Vector2f pos)
        {
            var rect = new RectangleShape(vector2F) {Position = pos};
            return rect;
        }
    }

    public class TopBar : IUpdatable, IControlContainer
    {
        public FloatRect GlobalBounds => _rect.GetGlobalBounds();

        private readonly string _text;
        private  string _dateText;
        private readonly Shape _rect;

        public TopBar(string text, float size)
        {
            _rect = ShapeHelper.RectangleWithColor(new Vector2f(size, 30.0f), Color.Yellow);
            _text = text;

            var speedUp = new Button("plus", new Vector2f(250.0f, 6.0f));
            speedUp.OnLeftClick += (sender, state) => Events.Event.NewChangeGameSpeed(1);
            var speedDown = new Button("minus", new Vector2f(300.0f, 6.0f));
            speedDown.OnLeftClick += (sender, state) => Events.Event.NewChangeGameSpeed(-1);

            ChildrenControls = new[] { speedUp, speedDown};
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);

            var h = new Text(_text, GlobalAssets.FontBold, 15);
            var pos = VectorHelper.centerPos(this._rect.GetGlobalBounds());
            var adjustedPos = VectorHelper.adjustPos(pos.Item1, pos.Item2, h.GetGlobalBounds());
            h.Position = new Vector2f(adjustedPos.Item1, adjustedPos.Item2);
            h.Color = Color.Black;
            target.Draw(h);

            var dateText = new Text(_dateText, GlobalAssets.FontBold, 12)
            {
                Position = new Vector2f(10.0f, 10.0f),
                Color = Color.Black
            };
            target.Draw(dateText);

            foreach (var ctrl in ChildrenControls)
            {
                target.Draw(ctrl);

            }
        }

        public void Update(GameState.GameState state)
        {
            _dateText = state.Date.ToString("dd MMMM yyyy -> H:mm") + " -> " +state.GameSpeed;
        }

        public IEnumerable<IBaseControl> ChildrenControls { get; }
        public Vector2f Position => _rect.Position;
    }
}
