using SFML.Graphics;
using SFML.Window;

namespace Renderer
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowSize = 800u;
            var frameRate = 60u;

            var mainWindow = new RenderWindow(new VideoMode(windowSize, windowSize), "EmptySpace");
            mainWindow.SetFramerateLimit(frameRate);
            mainWindow.Closed += (sender, eventArgs) => ((RenderWindow)sender).Close();

            var state = new GameState();

            while (true)
            {
                mainWindow.Clear();
                mainWindow.DispatchEvents();
                mainWindow.Draw(state.CurrentView);
                state = state.CurrentView.HandleEvents(mainWindow, state);
                mainWindow.Display();
            }
        }
    }

    internal class ViewHandler
    {
        public GameState Render(GameState state)
        {
            throw new System.NotImplementedException();
        }
    }
}
