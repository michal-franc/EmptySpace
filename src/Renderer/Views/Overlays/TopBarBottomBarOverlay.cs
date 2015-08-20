using Renderer.Controls;

namespace Renderer.Views.Overlays
{
    public class TopBarBottomBarOverlay : GameView
    {
        public override string Name => "TopBarBottomBarOverlay";

        private readonly string _name;

        public TopBarBottomBarOverlay(string name, float sizeX, float sizeY)
        {
            _name = name;
            base.Add(new TopBar(_name, sizeX));
            base.Add(new BottomBar(sizeX, sizeY));
        }
    }
}
