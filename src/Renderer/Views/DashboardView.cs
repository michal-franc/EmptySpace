namespace Renderer.Views
{
    public class DashboardView : GameView
    {
        public DashboardView(GameState state) : base()
        {
        }

        public override string Name => "DashBoard";
    }

    public class ShipView : GameView
    {
        public ShipView(GameState state) : base()
        {
        }

        public override string Name => "Ship - 'Glorificatus'";
    }
}
