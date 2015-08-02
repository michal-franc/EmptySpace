using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    // TODO: derive special control from control base that do have clicabke and hoverable option
    // TODO: Control base should just be the base without additional functionality
    // TODO: Move relation parent - child to some control base
    public abstract class ControlBase : Drawable
    {
        public abstract FloatRect GetGlobalBounds();
        protected abstract Drawable MainObj { get; }

        protected string Hint { get; private set; }
        public Vector2f Position { get; private set; }

        public event OnHoverHandler OnHover;
        public event OnClickHandler OnClick;

        public ControlBase(string hint, Vector2f position)
        {
            Hint = hint;
            Position = position;

            this.OnHover += (sender, hover) =>
            {
                DrawHint(sender);
            };
        }

        public virtual GameState ExecuteEvents(RenderTarget target, GameState state)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                var wnd = ((RenderWindow)target);
                var mPos = wnd.MapPixelToCoords(Mouse.GetPosition(wnd), wnd.GetView());
                var mouse = new FloatRect(mPos.X, mPos.Y, 1.0f, 1.0f);
                if (mouse.Intersects(this.GetGlobalBounds()))
                {
                    if (this.OnClick != null)
                    {
                        return this.OnClick(target, state);
                    }
                }
            }

            return state;
        }

        public virtual void DrawHint(RenderTarget target)
        {
            var hint = new Text(this.Hint, GlobalAssets.FontBold, 20);
            hint.Style = Text.Styles.Bold;
            hint.Position = new Vector2f(this.Position.X, this.Position.Y - 25.0f);
            hint.Color = Color.White;
            target.Draw(hint);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            var wnd = ((RenderWindow) target);
            wnd.Draw(this.MainObj);

            var mPos = wnd.MapPixelToCoords(Mouse.GetPosition(wnd), wnd.GetView());
            var mouse = new FloatRect(mPos.X, mPos.Y, 1.0f, 1.0f);
            if (mouse.Intersects(this.GetGlobalBounds()))
            {
                this.OnHover?.Invoke(target, null);
            }
        }
    }

    public class ControlSpriteBase : ControlBase
    {
        public Sprite Sprite { get; private set; }

        public ControlSpriteBase(Vector2f position, Sprite sprite, string hint) 
            : base(hint, position)
        {
            this.Sprite = sprite;
        }

        public override FloatRect GetGlobalBounds()
        {
            return this.Sprite.GetGlobalBounds();
        }

        protected override Drawable MainObj => this.Sprite;
    }

    public class ControlShapeBase : ControlBase
    {
        protected Shape Shape { get; private set; }

        public ControlShapeBase(Vector2f position, Shape shape, string hint)
            :base(hint, position)
        {
            this.Shape = shape;
        }

        public override FloatRect GetGlobalBounds()
        {
            return this.Shape.GetGlobalBounds();
        }

        protected override Drawable MainObj => this.Shape;
    }
}