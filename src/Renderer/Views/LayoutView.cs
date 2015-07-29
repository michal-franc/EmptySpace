using Renderer.Controls;
using SFML.Graphics;

namespace Renderer.Views
{
    public class LayoutView : GameView
    {
        public override string Name => "LayoutView";

        private readonly string _name;

        public LayoutView(string name)
        {
            _name = name;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Add(new TopBar(_name, target.Size.X));
            base.Add(new BottomBar(target.Size.X, target.Size.Y));
            base.Draw(target, states);
        }
    }
}
