using System;
using System.Collections.Generic;
using Renderer.StateEvents;
using Renderer.Views.Partial;
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

            var eventHandler = new GameStateEventHandler();

            var state = GameEngine.create();
            var viewState = new ViewState(state);

            mainWindow.MouseButtonReleased += (sender, eventArgs) => viewState.UnblockClick();

            float tickCounter = 0;

            while (true)
            {
                var currentEvents = new List<IViewStateChangeEvent>();
                mainWindow.Clear();
                mainWindow.DispatchEvents();

                viewState.CurrentView.UpdateControls(state);

                mainWindow.Draw(viewState.CurrentView);

                currentEvents.AddRange(viewState.CurrentView.HandleEvents(mainWindow, viewState));

                mainWindow.SetView(mainWindow.DefaultView);

                var layoutGameView = new LayoutView(viewState.CurrentView.Name, mainWindow.Size.X, mainWindow.Size.Y);
                layoutGameView.UpdateControls(state);
                currentEvents.AddRange(layoutGameView.HandleEvents(mainWindow, viewState));
                mainWindow.Draw(layoutGameView);

                currentEvents.AddRange(layoutGameView.HandleEvents(mainWindow, viewState));
                state = eventHandler.Consume(currentEvents, state);

                tickCounter++;
                if (tickCounter > GameEngine.speed(state))
                {
                    state = GameEngine.tick(state);
                    tickCounter = 0;
                }

                mainWindow.Display();

                viewState.ChangeState(state);
            }
        }
    }

    internal class GameStateEventHandler
    {
        public GameEngine.GameState Consume(IEnumerable<IViewStateChangeEvent> currentEvents, GameEngine.GameState state)
        {
            foreach (var eve in currentEvents)
            {
                state = eve.Apply(state);
            }

            return state;
        }
    }
}
