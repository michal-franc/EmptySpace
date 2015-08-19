namespace Renderer.StateEvents
{
    public class ExploreSystem : IViewStateChangeEvent
    {
        private readonly string _eventName;
        private readonly string _eventMessage;
        private readonly int _systemId;

        public ExploreSystem(string eventName, string eventMessage, int systemId)
        {
            _eventName = eventName;
            _eventMessage = eventMessage;
            _systemId = systemId;
        }

        public string EventName => _eventName;
        public string EventMessage => _eventMessage;

        public GameEngine.GameState Apply(GameEngine.GameState currentState)
        {
            return GameEngine.setExplored(currentState, _systemId);
        }
    }
}