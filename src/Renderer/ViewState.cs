using Renderer.StateEvents;
using Renderer.Views;

namespace Renderer
{
    public class ViewState
    {
        public IGameView CurrentView { get; private set; }
        private ViewType _currentViewType;

        private GameEngine.GameState _state;
        public GameEngine.GameState State => _state;

        public ViewState(GameEngine.GameState state)
        {
            this._state = state;
            this.CurrentView = new GalaxyView(this);
            this._currentViewType = ViewType.Galaxy;
        }

        public ViewState ChangeView(ViewType newView, object data = null)
        {
            switch (newView)
            {
                    case ViewType.Dashboard:
                        CurrentView = new DashboardView(this);
                        break;
                    case ViewType.Ship:
                        CurrentView = new ShipView(this);
                        break;
                    case ViewType.System:
                        CurrentView = new SystemView((StarSystem.StarSystem)data);
                        break;
                    case ViewType.Galaxy:
                        CurrentView = new GalaxyView(this);
                        break;
            }

            this._currentViewType = newView;

            return this;
        }

        public ViewState ChangeState(GameEngine.GameState state)
        {
            _state = state;
            return this;
        }
    }
}
