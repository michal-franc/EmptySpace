namespace Renderer.Views
{
    public class DashboardView : GameView
    {
        public DashboardView(ViewState state) : base()
        {
        }

        public override string Name => "DashBoard";
    }

    public class ShipView : GameView
    {
        public ShipView(ViewState state) : base()
        {
        }

        public override string Name => "Ship - 'Glorificatus'";
    }
}
