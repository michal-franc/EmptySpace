using Renderer.Controls.Panels;
using SFML.Window;

namespace Renderer.Views
{
    public class DashboardView : GameView
    {
        public DashboardView()
        {
            base.Add(new UpdatablePanel("Storage", new Vector2f(100.0f, 100.0f), 200.0f, 150.0f, (s) => Storage.print(s.Ship.Storage)));
            base.Add(new UpdatablePanel("Crew List", new Vector2f(500.0f, 100.0f), 600.0f, 150.0f, (s) => CrewList.print(s.Ship.CrewList)));
        }

        public override string Name => "DashBoard";
    }

    public class ShipView : GameView
    {
        public ShipView()
        {
        }

        public override string Name => "Ship - 'Glorificatus'";
    }
}
