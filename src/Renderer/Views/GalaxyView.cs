using Renderer.Controls.Buttons;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public class GalaxyView: GameView
    {
        public override string Name => "Galaxy";

        private View mainView;

        public GalaxyView(Universe.Universe universe)
        {
            this.mainView = new View(new FloatRect(0.0f, 0.0f, 1920, 900));
            mainView.Viewport = new FloatRect(0.0f, 0.0f, 1.0f, 1.0f);

            foreach (var starSystem in universe.Systems)
            {
                var rect = new StarControl(starSystem);
                rect.OnClick += (sender, state) => state.ChangeView(new SystemView(starSystem));
                base.Add(rect);
            }
        }
        
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GlobalAssets.SpaceBackground);

            var window = ((RenderWindow)target);
            window.MouseWheelMoved -= MoseWheelMoved;
            window.MouseWheelMoved += MoseWheelMoved;

            window.KeyPressed -= WindowOnKeyPressed;
            window.KeyPressed += WindowOnKeyPressed;

            target.SetView(mainView);
            base.Draw(target, states);
        }

        private void MoseWheelMoved(object sender, MouseWheelEventArgs args)
        {
            if (args.Delta > 0)
            {
                mainView.Zoom(1.4f);
            }
            else
            {
                mainView.Zoom(1.0f / 1.4f);
            }
        }


        private void WindowOnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Code == Keyboard.Key.L) { mainView.Move(new Vector2f(60.0f, 0.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.J) { mainView.Move(new Vector2f(0f, -60.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.K) { mainView.Move(new Vector2f(0f, 60.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.H) { mainView.Move(new Vector2f(-60.0f, .0f)); }
        }
    }
}