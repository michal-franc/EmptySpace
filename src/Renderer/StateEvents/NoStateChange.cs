namespace Renderer.StateEvents
{
    public class NoStateChange : IViewStateChangeEvent
    {
        private readonly string _eventName;
        private readonly string _eventMessage;

        public NoStateChange(string eventName, string eventMessage)
        {
            _eventName = eventName;
            _eventMessage = eventMessage;
        }

        public string EventName => _eventName;
        public string EventMessage => _eventMessage;

        public GameEngine.GameState Apply(GameEngine.GameState currentState)
        {
            return currentState;
        }
    }
}