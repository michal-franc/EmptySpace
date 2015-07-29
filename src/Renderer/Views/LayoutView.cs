using Renderer.Controls;
using SFML.Graphics;

namespace Renderer.Views
{
    public class LayoutView : GameView
    {
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Add(new TopBar("TopBar", target.Size.X));
            base.Add(new BottomBar(target.Size.X, target.Size.Y));
            base.Draw(target, states);
        }
    }
}
