using System;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Panels
{
    //TODO: Configurable Font, Size, Style, padding
    //TODO: Wrap words correctly without split-up
    public class WrappedTextContainer : ControlBase
    {
        private readonly Vector2f _padding;
        private readonly float _maxWidth;
        private Text _text;
        protected override Drawable MainObj => _text;

        public WrappedTextContainer(string text, Vector2f position, Vector2f padding, float maxWidth) : base(string.Empty, position)
        {
            _padding = padding;
            _maxWidth = maxWidth;
            _text = new Text(this.WrapText(text), GlobalAssets.FontNormal, 11);
            _text.Color = Color.White;
            _text.Position = position + padding;
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

        public override FloatRect GetGlobalBounds()
        {
            return this._text.GetGlobalBounds();
        }
    }
}
