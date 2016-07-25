using System.Collections;
using System.Collections.Generic;
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
            base.Add(new UpdatablePanel("Log", new Vector2f(1600.0f, 100.0f), 400.0f, 100.0f, state => this.CreateLog(state.Alerts)));
            base.Add(new TopBar(_name, sizeX));
            base.Add(new BottomBar(sizeX, sizeY));
        }

        private string CreateLog(IEnumerable<string> logs)
        {
            if (logs.Any())
            {
                return logs.Take(5).Aggregate((x, acc) => acc + "\n" + x);
            }
            else return string.Empty;
        }
    }
}
