using System;
using System.Collections.Generic;
using Renderer.Controls;
using Renderer.Controls.Buttons;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public interface IGameView : Drawable
    {
        GameState HandleEvents(RenderWindow mainWindow, GameState currentState);
    }

    public abstract class GameView : IGameView
    {
        private IList<ControlBase> controls;
        public Dictionary<ControlBase, Func<bool>> conditionalControls;

        public GameView()
        {
            controls = new List<ControlBase>();
            conditionalControls = new Dictionary<ControlBase, Func<bool>>();
        }

        public void Add(ControlBase control, Func<bool> condition = null)
        {
            if (condition == null)
            {
                this.controls.Add(control);
            }
            else
            {
                this.conditionalControls.Add(control, condition);
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var b in controls)
            {
                target.Draw(b);
            }

            foreach (var conditionalControl in conditionalControls)
            {
                if (conditionalControl.Value())
                {
                    target.Draw(conditionalControl.Key);
                }
            }
        }

        public GameState HandleEvents(RenderWindow mainWindow, GameState currentState)
        {
            foreach (var b in controls)
            {
                currentState = b.ExecuteEvents(mainWindow, currentState);
            };

            return currentState;
        }
    }
}