using System.Security.Policy;
using SFML.Window;

namespace Renderer.StateEvents
{
    public class ChangeGameSpeed : IViewStateChangeEvent
    {
        private readonly int _value;
        public string EventName => "ChangeGameSpeed";
        public string EventMessage => "Player has changed speed of the game";

        public ChangeGameSpeed(int value)
        {
            _value = value;
        }

        public GameEngine.GameState Apply(GameEngine.GameState currentState)
        {
            return GameEngine.changeSpeed(currentState, _value);
        }
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
            return GameEngine.startTravel(currentState, _newPos);
        }
    }
}