using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public abstract class Clickable : IClickable
    {
        public event OnClickHandler OnLeftClick;
        public Events.Event LeftClick(RenderTarget target, ViewState state)
        {
            if (OnLeftClick != null) { return this.OnLeftClick(target, state); }
            return Events.Event.NoStateChange;
        }

        public event OnClickHandler OnRightClick;
        public Events.Event RightClick(RenderTarget target, ViewState state)
        {
            if (OnRightClick != null) { return this.OnRightClick(target, state); }
            return Events.Event.NoStateChange;
        }

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract FloatRect GlobalBounds { get; }
    }

    public interface IClickable : IBaseControl
    {
        event OnClickHandler OnLeftClick;
        event OnClickHandler OnRightClick;
        Events.Event LeftClick(RenderTarget target, ViewState state);
        Events.Event RightClick(RenderTarget target, ViewState state);
    }
}