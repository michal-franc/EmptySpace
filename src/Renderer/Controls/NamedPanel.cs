using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Renderer.Controls.Panels;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    //TODO: Add drawable child controls in here so that i can dynamicaly create panels with elements
    //TODO: Stackable panels like in wpf so that layout is automatic
    public class NamedPanel : ControlBase
    {
        public  IEnumerable<ControlBase> ChildControls => _childControls;
        public  Vector2f Position => _position;
        private ICollection<ControlBase> _childControls;

        private readonly string _panelName;
        private readonly Vector2f _position;
        private readonly float _width;
        private readonly float _height;
        private RectangleShape _backgroundRect;

        public NamedPanel(string panelName, Vector2f position, float width, float height) : base(string.Empty, position)
        {
            _panelName = panelName;
            _position = position;
            _width = width;
            _height = height;

            _backgroundRect = new RectangleShape(new Vector2f(_width, _height));
            _backgroundRect.Position = _position;
            _backgroundRect.FillColor = new Color(0, 0, 0, 80);
            _backgroundRect.OutlineColor = new Color(255, 255, 255, 100);
            _backgroundRect.OutlineThickness = 1;
            _childControls = new Collection<ControlBase>();
        }

        public override FloatRect GetGlobalBounds()
        {
            return _backgroundRect.GetGlobalBounds();
        }

        protected override Drawable MainObj => _backgroundRect;
        
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            var panelNameText = new Text(_panelName, GlobalAssets.FontNormal, 11);
            panelNameText.Color = Color.White;
            var yx = panelNameText.GetGlobalBounds().Height;
            var w = panelNameText.GetGlobalBounds().Width;
            panelNameText.Position = new Vector2f(_position.X + (_width / 2.0f) - (w / 2.0f), _position.Y - (yx));
            target.Draw(panelNameText);

            foreach (var c in _childControls)
            {
               target.Draw(c); 
            }
        }

        public void AddChild(ControlBase control)
        {
            _childControls.Add(control);
        }
    }
}