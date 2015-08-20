using System.Collections.Generic;
using Renderer.Controls.Base;
using Renderer.Controls.Buttons;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    public enum ViewType
    {
        Dashboard,
        Ship,
        Galaxy,
        System
    }

    public class BottomBar : IControlContainer
    {
        public IEnumerable<IBaseControl> ChildrenControls { get; }
        public Vector2f Position => _rect.Position;
        public FloatRect GlobalBounds => _rect.GetGlobalBounds();
        private readonly Shape _rect;

        public BottomBar(float sizex, float sizey)
        {
            var pos = new Vector2f(0.0f, sizey - 50.0f);
            _rect = ShapeHelper.Rectangle(new Vector2f(sizex, 50.0f), pos, Color.Blue);

            var dashBtn = new Button("Dashboard", new Vector2f(10.0f, 10.0f + pos.Y));
            dashBtn.OnLeftClick += (sender, state) =>
            {
                state.ChangeView(ViewType.Dashboard);
                return Events.Event.NoStateChange;
            };

            var shipBtn = new Button("Ship", new Vector2f(100.0f, 10.0f + pos.Y));
            shipBtn.OnLeftClick += (sender, state) =>
            {
                state.ChangeView(ViewType.Ship);
                return Events.Event.NoStateChange; ;
            };

            var galaxyBtn = new Button("Galaxy", new Vector2f(200.0f, 10.0f + pos.Y));
            galaxyBtn.OnLeftClick += (sender, state) =>
            {
                state.ChangeView(ViewType.Galaxy);
                return Events.Event.NoStateChange;
            };

            ChildrenControls = new[] { dashBtn, shipBtn, galaxyBtn };
        }

        // I have over-engineered previous code, i am refactoring the mess now
        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);

            foreach (var ctrl in ChildrenControls)
            {
                target.Draw(ctrl);
            }
        }
    }
}
