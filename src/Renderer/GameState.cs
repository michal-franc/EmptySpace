using Renderer.Views;

namespace Renderer
{
    public class GameState
    {
        public IGameView CurrentView { get; private set; }
        public Universe.Universe Universe { get; }

        public GameState()
        {
            this.Universe = GalaxyGenerator.generate;
            CurrentView = new GalaxyView(Universe);
        }

        public GameState ChangeView(IGameView selectedView)
        {
            CurrentView = selectedView;
            return this;
        }
    }
}
