﻿using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Panels
{
    // TODO: nth -> inject text object instead of string to have a wayt to inject all the styles, font, sizes etc
    // TODO: configurable nameplate color
    // TODO: somehow create two types of this object, one which can be a main object the other one which needs allways a parent
    public class NamePlateContainer : ControlBase
    {
        private readonly string _panelName;
        private readonly Vector2f _padding;
        private readonly Vector2f _position;
        private readonly float _width;

        public NamePlateContainer(string panelName, Vector2f padding, ControlBase parent) : base(string.Empty, parent.Position + padding)
        {
            _panelName = panelName;
            _padding = padding;
            _position = parent.Position;
            _width = parent.GetGlobalBounds().Width;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            var namePlateText = new Text(_panelName, GlobalAssets.FontNormal, 11);
            namePlateText.Color = Color.White;
            namePlateText.Position = new Vector2f(_position.X + (_width / 2.0f) - (namePlateText.GetGlobalBounds().Width / 2.0f), _position.Y + _padding.Y);

            var namePlateBack = new RectangleShape(new Vector2f(_width, namePlateText.GetGlobalBounds().Height + 2));
            namePlateBack.Position = new Vector2f(_position.X, _position.Y + _padding.Y);
            namePlateBack.FillColor = new Color(13, 41, 51, 255);

            target.Draw(namePlateBack);
            target.Draw(namePlateText);
        }

        public override FloatRect GetGlobalBounds()
        {
            return new FloatRect();
        }

        //TODO: hack with introduction of proper control base i will be able to remove this madness
        protected override Drawable MainObj => new Text();
    }
}