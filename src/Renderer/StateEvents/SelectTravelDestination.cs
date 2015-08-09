using SFML.Window;

namespace Renderer.StateEvents
{
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
            return GameEngine.startTravel(currentState, _newPos);
        }
    }
}