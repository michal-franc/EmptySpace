using SFML.Window;

namespace Renderer.StateEvents
{
    public enum ViewType
    {
        Dashboard = 0,
        Ship,
        Galaxy,
        System
    }

    public interface IViewStateChangeEvent
    {
        string EventName { get; }
        string EventMessage { get; }
        GameEngine.GameState Apply(GameEngine.GameState currentState);
    }

    public class SelectTravelDestination : IViewStateChangeEvent
    {
        private readonly Vector2f _newPos;
        public string EventName => "SelectTravelDestination";
        public string EventMessage => "Player has selected new destination";

        public SelectTravelDestination(Vector2f newPos)
        {
            _newPos = newPos;
        }

        public GameEngine.GameState Apply(GameEngine.GameState currentState)
        {
            return GameEngine.travel(currentState, _newPos);
        }
    }
}