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

    public class ViewChange : IViewStateChangeEvent
    {
        private object _viewData;

        public string EventName => "View Change";
        public string EventMessage => "View has been changed";

        public object ViewData => _viewData;
        
        public ViewChange(object viewData)
        {
            _viewData = viewData;
        }


        public GameEngine.GameState Apply(GameEngine.GameState currentState)
        {
            return currentState;
        }
    }
}