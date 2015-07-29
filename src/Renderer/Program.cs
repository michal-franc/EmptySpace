using Renderer.Views;
using SFML.Graphics;
using SFML.Window;

namespace Renderer
{
    class Program
    {
        static void Main(string[] args)
        {
            var frameRate = 60u;

            var mainWindow = new RenderWindow(new VideoMode(1920, 900), "EmptySpace", Styles.Resize);
            mainWindow.SetFramerateLimit(frameRate);
            mainWindow.Closed += (sender, eventArgs) => ((RenderWindow)sender).Close();

            mainWindow.KeyPressed += (sender, eventArgs) =>
            {
                if (eventArgs.Code == Keyboard.Key.Escape)
                {
                    ((RenderWindow) sender).Close();
                }
            };

            var state = new GameState();

            

            while (true)
            {
                mainWindow.Clear();
                mainWindow.DispatchEvents();
                mainWindow.Draw(state.CurrentView);
                state = state.CurrentView.HandleEvents(mainWindow, state);
                mainWindow.SetView(mainWindow.DefaultView);

                var layoutGameView = new LayoutView(state.CurrentView.Name);
                mainWindow.Draw(layoutGameView);
                state = layoutGameView.HandleEvents(mainWindow, state);

                mainWindow.Display();
            }
        }
    }
}
