using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    class SpriteButton : IBaseControl, IClickable, IHoverable
    {
        private readonly Sprite _sprite;
        private readonly string _hint;
        public SpriteButton(Sprite sprite, string hint)
        {
            _sprite = sprite;
            _hint = hint;
        }

        // TODO: Move to ISelectable ? ( but there migh be different selectable groups )
        public void DrawSelected(RenderTarget target)
        {
            target.Draw(this.CreateHint(_hint));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_sprite);
        }

        public event OnClickHandler OnClick;
        public GameState Click(RenderTarget target, GameState state)
        {
            if (OnClick != null) { return this.OnClick(target, state); }
            return state;
        }

        public event OnHoverHandler OnHover;
        public void Hover(RenderTarget target)
        {
            if (OnHover != null) { this.OnHover(target, null); }

            target.Draw(this.CreateHint(_hint));
        }

        private Drawable CreateHint(string hintText)
        {
            var hint = new Text(hintText, GlobalAssets.FontBold, 15);
            hint.Style = Text.Styles.Bold;
            hint.Position = new Vector2f(this._sprite.Position.X, this._sprite.Position.Y - 25.0f);
            hint.Color = Color.White;
            return hint;
        }

        public FloatRect GetGlobalBounds()
        {
            return this._sprite.GetGlobalBounds();
        }
    }
}