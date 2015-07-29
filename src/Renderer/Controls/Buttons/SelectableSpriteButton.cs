using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Buttons
{
    class SpriteButton : ControlSpriteBase
    {
        public SpriteButton(Sprite sprite, string hint) :
            base(sprite.Position, sprite, hint)
        {
        }

        public void DrawSelected(RenderTarget target)
        {
            this.DrawHint(target);
        }
    }
}