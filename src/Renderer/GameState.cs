using Renderer.Views;
using SFML.Window;

namespace Renderer
{
    public class GameState
    {
        public IGameView CurrentView { get; private set; }
        public Universe.Universe Universe { get; }
        public Vector2f PlayerPosition { get; }

        public GameState()
        {
            this.Universe = GalaxyGenerator.generate;
            //TODO: make it better here :) ( make it so that star is in the middle )
            this.PlayerPosition = this.Universe.Systems[0].Position;
            CurrentView = new GalaxyView(this);
        }

        public GameState ChangeView(IGameView selectedView)
        {
            CurrentView = selectedView;
            return this;
        }
    }
}
