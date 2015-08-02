using System;
using System.Collections.Generic;
using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public interface IGameView : Drawable
    {
        GameState HandleEvents(RenderWindow mainWindow, GameState currentState);
        string Name { get; }
    }

    public abstract class GameView : IGameView
    {
        public abstract string Name { get; }

        private IList<IBaseControl> _controls;
        public Dictionary<IBaseControl, Func<bool>> _conditionalControls;

        public GameView()
        {
            _controls = new List<IBaseControl>();
            _conditionalControls = new Dictionary<IBaseControl, Func<bool>>();
        }

        public void Add(IBaseControl control, Func<bool> condition = null)
        {
            if (condition == null)
            {
                this._controls.Add(control);
            }
            else
            {
                this._conditionalControls.Add(control, condition);
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var b in _controls)
            {
                target.Draw(b);
            }

            foreach (var conditionalControl in _conditionalControls)
            {
                if (conditionalControl.Value())
                {
                    target.Draw(conditionalControl.Key);
                }
            }
        }


        public GameState HandleEvents(RenderWindow mainWindow, GameState currentState)
        {
            foreach (var c in _controls)
            {
                currentState = this.HandleEventsRec(mainWindow, currentState, c);
            }

            return currentState;
        }

        private GameState HandleEventsRec(RenderWindow mainWindow, GameState currentState, IBaseControl ctrl)
        {
            // TODO: how to do now double click event ? click event ? right click event ?
            // TODO: click event handler will have click event args (left, right, double ) and i will be able to decide what to do with it
            // TODO: i will probably have to implement double click by myself ( in my scenario i dont need double click but just left click with different action based on context )
            var mPos = mainWindow.MapPixelToCoords(Mouse.GetPosition(mainWindow), mainWindow.GetView());
            var mouse = new FloatRect(mPos.X, mPos.Y, 1.0f, 1.0f);
            if (mouse.Intersects(ctrl.GlobalBounds))
            {
                var c = ctrl as IClickable;
                if (c != null)
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        currentState = c.Click(mainWindow, currentState);
                    }
                }

                var h = ctrl as IHoverable;
                h?.Hover(mainWindow);
            }

            var p = ctrl as IControlContainer;
            if (p != null)
            {
                foreach (var child in p.ChildrenControls)
                {
                    this.HandleEventsRec(mainWindow, currentState, child);
                }
            }

            return currentState;
        }
    }
}