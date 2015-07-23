using SFML.Graphics;

namespace Renderer
{
    public interface IGameView : Drawable
    {
        GameState HandleEvents(RenderWindow mainWindow, GameState currentState);
    }
}