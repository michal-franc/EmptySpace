using System;
using Renderer.Controls.Buttons;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public static class Vector2fExtensions
    {
        public static bool IsZero(this Vector2f vect)
        {
            return vect.X == 0.0f && vect.Y == 0.0f;
        }

        public static Vector2f Apply(this Vector2f vect, Func<float, float> apply)
        {
            vect.X = apply(vect.X);
            vect.Y = apply(vect.Y);
            return vect;
        }

        public static Vector2f Normalize(this Vector2f vect)
        {
            Func<float, float> abs = (val) =>
            {
                var wasNegative = val > 0.0f;
                val = (float) Math.Sqrt(Math.Abs(val));
                return wasNegative ? val : -val;
            };

            return vect.Apply(abs);
        }
    }

    public class GalaxyView: GameView
    {
        public override string Name => "Galaxy";
        private Vector2f _mouseDragStartPosition;

        private View mainView;

        public GalaxyView(Universe.Universe universe)
        {
            this.mainView = new View(new FloatRect(0.0f, 0.0f, 1920, 900));
            mainView.Viewport = new FloatRect(0.0f, 0.0f, 1.0f, 1.0f);

            foreach (var starSystem in universe.Systems)
            {
                var rect = new StarControl(starSystem);
                rect.OnClick += (sender, state) => state.ChangeView(new SystemView(starSystem));
                base.Add(rect);
            }
        }
        
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GlobalAssets.SpaceBackground);

            var window = ((RenderWindow)target);

            window.MouseWheelMoved -= MoseWheelMoved;
            window.MouseWheelMoved += MoseWheelMoved;

            window.KeyPressed -= WindowOnKeyPressed;
            window.KeyPressed += WindowOnKeyPressed;

            window.MouseButtonPressed -= OnWindowOnMouseButtonPressed;
            window.MouseButtonPressed += OnWindowOnMouseButtonPressed;

            window.MouseMoved -= OnWindowOnMouseMoved;
            window.MouseMoved += OnWindowOnMouseMoved;

            target.SetView(mainView);
            base.Draw(target, states);
        }

        private void OnWindowOnMouseMoved(object sender, MouseMoveEventArgs args)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                var curPos = new Vector2f(args.X, args.Y);;

                var diff = _mouseDragStartPosition - curPos;
                if (!diff.IsZero())
                {
                    var multiplier = 2.0f + (this.mainView.Size.X / 1000.0f) ;
                    diff = diff.Normalize();
                    diff *= multiplier;

                    this.mainView.Move(diff);
                }

                _mouseDragStartPosition = curPos;
            }
        }

        private void OnWindowOnMouseButtonPressed(object sender, MouseButtonEventArgs args)
        {
            if (args.Button == Mouse.Button.Right)
            {
                _mouseDragStartPosition = new Vector2f(args.X, args.Y);
            }
        }

        private void MoseWheelMoved(object sender, MouseWheelEventArgs args)
        {
            if (args.Delta > 0)
            {
                mainView.Zoom(1.4f);
            }
            else
            {
                mainView.Zoom(1.0f / 1.4f);
            }
        }


        private void WindowOnKeyPressed(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Code == Keyboard.Key.L) { mainView.Move(new Vector2f(60.0f, 0.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.J) { mainView.Move(new Vector2f(0f, -60.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.K) { mainView.Move(new Vector2f(0f, 60.0f)); }
            if (keyEventArgs.Code == Keyboard.Key.H) { mainView.Move(new Vector2f(-60.0f, .0f)); }
        }
    }
}