using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Panels
{
    // TODO: create special base class or interface to indicate that this control is a child control ( those control wont be usable without parent )
    // TODO: this will require different font so that all the chars have the same size (Arial ? )
    // TODO: data dict has to be more complicated with info about color of value, style, etc
    // TODO: this logic can be moved to F#
    public class LeftRightAlignedText : ControlBase
    {
        private readonly IDictionary<string, string> _data; 
        private readonly Vector2f _position;
        private readonly Vector2f _padding;
        private readonly float _width;
        private Text _text;
        protected override Drawable MainObj => _text;

        public LeftRightAlignedText(IDictionary<string, string> data, Vector2f padding, ControlBase parent) : base(string.Empty, parent.Position + padding)
        {
            _data = data;
            _position = parent.Position + padding;
            _padding = padding;
            _width = parent.GetGlobalBounds().Width;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            _text = new Text(this.CreateLeftRightAlignedText(), GlobalAssets.FontNormal, 11);
            _text.Color = Color.White;
            _text.Position = new Vector2f(_position.X, _position.Y);

            base.Draw(target, states);
        }

        private string CreateLeftRightAlignedText()
        {
            var sb = new StringBuilder();
            
            var glyph = GlobalAssets.FontNormal.GetGlyph(' ', 11, false);
            var sizeOfSpace = glyph.Advance;
            
            foreach (var d in _data)
            {
                sb.Append(d.Key);

                var number = (_width - (this.SizeOfString(d.Key) + this.SizeOfString(d.Value) + (this._padding.X * 2.0f))) / sizeOfSpace;

                for (var i = 0; i < number; i++)
                {
                    sb.Append(' ');
                }

                sb.Append(d.Value);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        private float SizeOfString(string text)
        {
            var size = 0.0f;
            foreach (var c in text)
            {
                var glyph = GlobalAssets.FontNormal.GetGlyph(c, 11, false);
                size += glyph.Advance;
            }

            return size;
        }

        public override FloatRect GetGlobalBounds()
        {
            return new FloatRect();
        }
    }
}