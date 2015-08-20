using System;
using System.Text;
using Renderer.Controls.Base;
using Renderer.Controls.Buttons;
using Renderer.Resources;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Panels
{
    //TODO: Configurable Font, Size, Style 
    //TODO: Wrap words correctly without split-up
    //TODO: move wraped text logic to separate object without padding and parent and just maxWidth defined
    //padding and position will be then defined by parend on add
    public class WrappedTextContainer : IBaseControl
    {
        private readonly Vector2f _padding;
        private readonly float _maxWidth;
        private Text _text;

        public FloatRect GlobalBounds => _text.GetGlobalBounds();

        public WrappedTextContainer(string text, Vector2f padding, IControlContainer parent)
        {
            _padding = padding;
            _maxWidth = parent.GlobalBounds.Width;
            _text = new Text(this.WrapText(text), GlobalAssets.FontNormal, 11);
            _text.Color = Color.White;
            _text.Position = parent.Position + padding;
        }

        private string WrapText(string longText)
        {
            var sb = new StringBuilder();

            var currentOffset = _padding.X;
            foreach (var c in longText)
            {
                var glyph = GlobalAssets.FontNormal.GetGlyph(c, 11, false);
                currentOffset += glyph.Advance;
                if (currentOffset >= (_maxWidth - _padding.X))
                {
                    sb.Append(Environment.NewLine);
                    currentOffset = _padding.X;
                }
                sb.Append(c);
            }

            return sb.ToString();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_text);
        }

        //TODO: DRY
        public void UpdateText(string text)
        {
            var oldPos = _text.Position;
            _text = new Text(this.WrapText(text), GlobalAssets.FontNormal, 11);
            _text.Color = Color.White;
            _text.Position = oldPos;
        }
    }
}
