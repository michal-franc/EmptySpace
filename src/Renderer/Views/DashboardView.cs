using Renderer.Controls.Panels;
using SFML.Window;

namespace Renderer.Views
{
    public class DashboardView : GameView
    {
        public DashboardView(ViewState state)
        {
            var panel = new StoragePanel(new Vector2f(100.0f, 100.0f), 300.0f, 150.0f, state.State.Storage);
            base.Add(panel);
        }

        public override string Name => "DashBoard";
    }

    public class ShipView : GameView
    {
        public ShipView(ViewState state)
        {
        }

        public override string Name => "Ship - 'Glorificatus'";
    }
}
