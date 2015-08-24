using System.Linq;
using Renderer.Controls;
using Renderer.Controls.Panels;
using SFML.Window;

namespace Renderer.Views.Overlays
{
    public class TopBarBottomBarOverlay : GameView
    {
        public override string Name => "TopBarBottomBarOverlay";

        private readonly string _name;

        public TopBarBottomBarOverlay(string name, float sizeX, float sizeY)
        {
            _name = name;
            //TODO: move this to proper alert overlay when overlay system is in place
            base.Add(new UpdatablePanel("Log", new Vector2f(1600.0f, 100.0f), 300.0f, 50.0f, state => state.Alerts.FirstOrDefault()));
            base.Add(new TopBar(_name, sizeX));
            base.Add(new BottomBar(sizeX, sizeY));
        }
    }
}
