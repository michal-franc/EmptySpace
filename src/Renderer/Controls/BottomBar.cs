using System.Collections;
using System.Collections.Generic;
using Renderer.Controls.Buttons;
using Renderer.Views;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    public class BottomBar : ControlShapeBase
    {
        private IList<ControlBase> Controls;

        public BottomBar(float sizex, float sizey)
            : base(new Vector2f(0.0f, sizey - 50.0f), ShapeHelper.Rectangle(new Vector2f(sizex, 50.0f), new Vector2f(0.0f, sizey - 50.0f), Color.Blue), string.Empty)
        {
            this.Controls = new List<ControlBase>();
            var dashBtn = new Button("Dashboard", new Vector2f(10.0f, 10.0f + this.Position.Y));
            dashBtn.OnClick += (sender, state) => state.ChangeView(new DashboardView(state));
            this.Controls.Add(dashBtn);

            var shipBtn = new Button("Ship", new Vector2f(100.0f, 10.0f + this.Position.Y));
            shipBtn.OnClick += (sender, state) => state.ChangeView(new ShipView(state));
            this.Controls.Add(shipBtn);

            var galaxyBtn = new Button("Galaxy", new Vector2f(200.0f, 10.0f + this.Position.Y));
            galaxyBtn.OnClick += (sender, state) => state.ChangeView(new GalaxyView(state.Universe));
            this.Controls.Add(galaxyBtn);
        }

        public override GameState ExecuteEvents(RenderTarget target, GameState state)
        {
            foreach (var controlBase in Controls)
            {
                state = controlBase.ExecuteEvents(target, state);
            }
            return base.ExecuteEvents(target, state);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            foreach (var controlBase in Controls)
            {
                target.Draw(controlBase);
            }
        }
    }
}
