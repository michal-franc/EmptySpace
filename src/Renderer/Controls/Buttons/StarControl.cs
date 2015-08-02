using Renderer.Controls.Base;
using Renderer.CustomShapes;
using Renderer.Views;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    public class StarControl : IBaseControl, IClickable, IHoverable 
    {
        private Shape _rect;
        private StarSystem.Sun _sun;
        private Vector2f _position;

        public StarControl(StarSystem.StarSystem system)
        {
            this.OnClick += (sender, state) => state.ChangeView(new SystemView(system));

            _position = system.Position;
            _rect = new StarShape(system, new Vector2f(_position.X, _position.Y));
            _sun = system.Sun; 
        }

        public event OnClickHandler OnClick;
        public GameState Click(RenderTarget target, GameState state)
        {
            // TODO : I will have to think how to get rid of this -.-
            if (OnClick != null) { return this.OnClick(target, state); }
            return state;
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

        public FloatRect GetGlobalBounds()
        {
            return this._rect.GetGlobalBounds();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_rect);
        }
    }
}