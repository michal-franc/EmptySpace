using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public abstract class Clickable : IClickable
    {
        public event OnClickHandler OnLeftClick;
        public GameState LeftClick(RenderTarget target, GameState state)
        {
            if (OnLeftClick != null) { return this.OnLeftClick(target, state); }
            return state;
        }

        public event OnClickHandler OnRightClick;
        public GameState RightClick(RenderTarget target, GameState state)
        {
            if (OnRightClick != null) { return this.OnRightClick(target, state); }
            return state;
        }

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract FloatRect GlobalBounds { get; }
    }

    public interface IClickable : IBaseControl
    {
        event OnClickHandler OnLeftClick;
        event OnClickHandler OnRightClick;
        GameState LeftClick(RenderTarget target, GameState state);
        GameState RightClick(RenderTarget target, GameState state);
    }
}