using System.Xml.Schema;
using Renderer.Controls.Base;
using Renderer.CustomShapes;
using Renderer.Resources;
using Renderer.Views;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public class StarControl : Clickable, IHoverable
    {
        public Vector2f Position => _position;

        private StarSprite _rect;
        private StarSystem.Sun _sun;
        private Vector2f _position;

        public override FloatRect GlobalBounds => _rect.GetGlobalBounds();

        public StarControl(StarSystem.StarSystem system)
        {
            _position = system.Position;
            _rect = new StarSprite(system, new Vector2f(_position.X, _position.Y));
            _sun = system.Sun; 
        }

        public event OnHoverHandler OnHover;
        public void Hover(RenderTarget target)
        {
            if (OnHover != null) { this.OnHover(target, null); }

            target.Draw(this.CreateHint(this._sun.Name));
        }

        //TODO: Make the hint bigger when the galaxy zoom is smaller
        //TODO: change this to maybe IHintable :D? or something like that
        //TODO: remove the same logic duplication
        private Drawable CreateHint(string hintText)
        {
            var hint = new Text(hintText, GlobalAssets.FontBold, 15);
            hint.Style = Text.Styles.Bold;
            hint.Position = new Vector2f(this._position.X, this._position.Y - 25.0f);
            hint.Color = Color.White;
            return hint;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);
        }
    }
}