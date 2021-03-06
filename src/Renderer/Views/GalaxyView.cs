﻿using System;
using Microsoft.FSharp.Core;
using Renderer.Controls;
using Renderer.Controls.Buttons;
using Renderer.Controls.Panels;
using Renderer.Resources;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    //TODO: Move to F#

    public class GalaxyView: GameView
    {
        public override string Name => "Galaxy";
        private Vector2f _mouseDragStartPosition;
        private StarControl _selectedStar;

        private View mainView;

        public GalaxyView(GameState.GameState state)
        {
            this.mainView = new View(new FloatRect(0.0f, 0.0f, 1920, 900));
            mainView.Viewport = new FloatRect(0.0f, 0.0f, 1.0f, 1.0f);
            mainView.Center = state.Ship.Position;

            foreach (var starSystem in state.Universe.Systems)
            {
                var rect = new StarControl(starSystem);
                rect.OnLeftClick += (sender, gstate) => { _selectedStar = rect; return Events.Event.NoStateChange; };
                rect.OnRightClick += (sender, gameState) => 
                {
                   gameState.ChangeView(ViewType.System, new Tuple<StarSystem.StarSystem, bool>(starSystem, true));
                   return Events.Event.NoStateChange;
                };
                base.Add(rect);
            }

            //TODO: Show low fuel alert -> alert system
            base.Add(() => new StarSelectedIndicatorControl(_selectedStar.Position), () => _selectedStar != null);
            base.Add(() =>
            {
                // TODO: encapsulate this as aseparate control
                var panel = new NamedPanel("Star Name", _selectedStar.Position + new Vector2f(50.0f, -40.0f), 100.0f, 100.0f);

                //TODO: we need button that can have a parent so it can be positioned inside panel
                var btn = new Button("Travel", new Vector2f(10.0f, 50.0f), panel);
                btn.OnLeftClick += (sender, gameState) => Events.Event.NewTravelDest(new Tuple<Vector2f, int>(_selectedStar.Position, _selectedStar.Id));
                panel.AddChild(btn);
                return panel;
            }, () => _selectedStar != null);

            base.Add(new ShipIndicatorControl(state.Ship.Position));
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

        //TODO: Scrolling should not move the mouse cursor
        private void OnWindowOnMouseMoved(object sender, MouseMoveEventArgs args)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Right))
            {
                var curPos = new Vector2f(args.X, args.Y);;

                var diff = _mouseDragStartPosition - curPos;
                if (!VectorHelper.isZero(diff))
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
            if (args.Delta < 0)
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