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
        public Dictionary<Func<IBaseControl>, Func<bool>> _conditionalControls;

        public GameView()
        {
            _controls = new List<IBaseControl>();
            _conditionalControls = new Dictionary<Func<IBaseControl>, Func<bool>>();
        }

        public void Add(IBaseControl control)
        {
                this._controls.Add(control);
        }

        public void Add(Func<IBaseControl> funccontrol, Func<bool> funcbool)
        {
                this._conditionalControls.Add(funccontrol, funcbool);
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
                    target.Draw(conditionalControl.Key());
                }
            }
        }


        public GameState HandleEvents(RenderWindow mainWindow, GameState currentState)
        {
            foreach (var c in _controls)
            {
                currentState = this.HandleEventsRec(mainWindow, currentState, c);
            }

            //TODO: we are invoking and creating the same control all the time!
            foreach (var c in _conditionalControls)
            {
                if (c.Value())
                {
                    currentState = this.HandleEventsRec(mainWindow, currentState, c.Key());
                }
            }

            return currentState;
        }

        private GameState HandleEventsRec(RenderWindow mainWindow, GameState currentState, IBaseControl ctrl)
        {
            // TODO: how to do now double click event ? click event ? right click event ?
            // TODO: click event handler will have click event args (left, right, double ) and i will be able to decide what to do with it
            // TODO: i will probably have to implement double click by myself ( in my scenario i dont need double click but just left click with different action based on context )
            // TODO: Maybe expose event handlers, left click, right click ? ( Instead of one )
            var mPos = mainWindow.MapPixelToCoords(Mouse.GetPosition(mainWindow), mainWindow.GetView());
            var mouse = new FloatRect(mPos.X, mPos.Y, 1.0f, 1.0f);
            if (mouse.Intersects(ctrl.GlobalBounds))
            {
                var c = ctrl as IClickable;
                if (c != null)
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        currentState = c.LeftClick(mainWindow, currentState);
                    }
                    else if (Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        currentState = c.RightClick(mainWindow, currentState);
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