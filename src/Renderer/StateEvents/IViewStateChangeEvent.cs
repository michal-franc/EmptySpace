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
}