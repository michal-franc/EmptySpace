using System;
using Renderer.Controls;
using Renderer.Views;

namespace Renderer
{
    public class ViewState
    {
        public bool ClickBlocked { get; private set; }
        public IGameView CurrentView { get; private set; }
        private GameState.GameState _state;


        public ViewState(GameState.GameState state)
        {
            _state = state;
            CurrentView = new GalaxyView(state);
        }

        public ViewState ChangeView(ViewType newView,object data = null)
        {
            switch (newView)
            {
                    case ViewType.Dashboard:
                        CurrentView = new DashboardView();
                        break;
                    case ViewType.Ship:
                        CurrentView = new ShipView();
                        break;
                    case ViewType.System:
                        var tuple = (Tuple<StarSystem.StarSystem, bool>) data;
                        CurrentView = new SystemView(tuple.Item1, tuple.Item2);
                        break;
                    case ViewType.Galaxy:
                        CurrentView = new GalaxyView(_state);
                        break;
                    default:
                        CurrentView = new GalaxyView(_state);
                    break;
            }

            return this;
        }

        public ViewState ChangeState(GameState.GameState state)
        {
            _state = state;
            return this;
        }

        public void BlockClick()
        {
            this.ClickBlocked = true;
        }

        public void UnblockClick()
        {
            this.ClickBlocked = false;
        }
    }
}
