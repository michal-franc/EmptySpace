using Renderer.Controls.Base;
using Renderer.Resources;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    class SpriteButton : Clickable, IHoverable
    {
        private readonly Sprite _sprite;
        private readonly string _hint;

        public override FloatRect GlobalBounds => _sprite.GetGlobalBounds();

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

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_sprite);
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

    }
}