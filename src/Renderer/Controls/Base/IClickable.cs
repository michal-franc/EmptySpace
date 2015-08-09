using Renderer.StateEvents;
using Renderer.Views;
using SFML.Graphics;

namespace Renderer.Controls.Base
{
    public abstract class Clickable : IClickable
    {
        public event OnClickHandler OnLeftClick;
        public IViewStateChangeEvent LeftClick(RenderTarget target, ViewState state)
        {
            if (OnLeftClick != null) { return this.OnLeftClick(target, state); }
            return new NoStateChange("LeftClick", "User clicked left click "); ;
        }

        public event OnClickHandler OnRightClick;
        public IViewStateChangeEvent RightClick(RenderTarget target, ViewState state)
        {
            if (OnRightClick != null) { return this.OnRightClick(target, state); }
            return new NoStateChange("RightClick", "User clicked right click ");
        }

        public abstract void Draw(RenderTarget target, RenderStates states);

        public abstract FloatRect GlobalBounds { get; }
    }

    public interface IClickable : IBaseControl
    {
        event OnClickHandler OnLeftClick;
        event OnClickHandler OnRightClick;
        IViewStateChangeEvent LeftClick(RenderTarget target, ViewState state);
        IViewStateChangeEvent RightClick(RenderTarget target, ViewState state);
    }
}