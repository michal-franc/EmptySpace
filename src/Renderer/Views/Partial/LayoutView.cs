using Renderer.Controls;
using SFML.Graphics;

namespace Renderer.Views.Partial
{
    public class LayoutView : GameView
    {
        public override string Name => "LayoutView";

        private readonly string _name;

        public LayoutView(string name, float sizeX, float sizeY)
        {
            _name = name;
            base.Add(new TopBar(_name, sizeX));
            base.Add(new BottomBar(sizeX, sizeY));
        }
    }
}
