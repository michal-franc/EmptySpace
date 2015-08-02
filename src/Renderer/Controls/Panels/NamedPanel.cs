using System.Collections.Generic;
using System.Collections.ObjectModel;
using Renderer.Controls.Base;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls.Panels
{
    //TODO: Add drawable child controls in here so that i can dynamicaly create panels with elements
    //TODO: Stackable panels like in wpf so that layout is automatic
    public class NamedPanel : IControlContainer 
    {
        public  IEnumerable<IBaseControl> ChildrenControls => _childControls;
        public  Vector2f Position => _position;
        public  FloatRect GlobalBounds => _backgroundRect.GetGlobalBounds();
        private ICollection<IBaseControl> _childControls;

        private readonly string _panelName;
        private readonly Vector2f _position;
        private readonly float _width;
        private readonly float _height;
        private RectangleShape _backgroundRect;

        public NamedPanel(string panelName, Vector2f position, float width, float height)
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
            _childControls = new Collection<IBaseControl>();
        }

        
        public void Draw(RenderTarget target, RenderStates states)
        {
            var panelNameText = new Text(_panelName, GlobalAssets.FontNormal, 11);
            panelNameText.Color = Color.White;
            var yx = panelNameText.GetGlobalBounds().Height;
            var w = panelNameText.GetGlobalBounds().Width;
            panelNameText.Position = new Vector2f(_position.X + (_width / 2.0f) - (w / 2.0f), _position.Y - (yx));
            target.Draw(_backgroundRect);
            target.Draw(panelNameText);

            foreach (var c in _childControls)
            {
               target.Draw(c); 
            }
        }

        public void AddChild(IBaseControl control)
        {
            _childControls.Add(control);
        }
    }
}