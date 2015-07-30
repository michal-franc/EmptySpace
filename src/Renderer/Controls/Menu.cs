using System;
using System.Text;
using Renderer.Controls.Panels;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Controls
{
    public class Menu : ControlBase
    {
        private readonly Vector2f _position;
        private readonly float _width;
        private readonly float _height;

        public Menu(Vector2f position, float width, float height) : base("", position)
        {
            _position = position;
            _width = width;
            _height = height;
        }

        public override FloatRect GetGlobalBounds()
        {
            return new FloatRect();
        }

        protected override Drawable MainObj
        {
            get
            {
                var rect = new RectangleShape(new Vector2f(_width, _height));
                rect.Position = _position;
                rect.FillColor = new Color(0, 0, 0, 80);
                rect.OutlineColor = new Color(255, 255, 255, 100);
                rect.OutlineThickness = 1;
                return rect;
            }
        }

        
        // Experimental Mess
        // TODO: I need a control to easilly define a container that has a wrapped text
        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            var planetInfo = "Planet - Earth";
            var pText = new Text(planetInfo, GlobalAssets.FontNormal, 11);
            pText.Color = Color.White;

            var yx = pText.GetGlobalBounds().Height;
            var w = pText.GetGlobalBounds().Width;
            pText.Position = new Vector2f(_position.X + (_width / 2.0f) - ( w / 2.0f ), _position.Y - (yx));
            target.Draw(pText);

            var longText = "A desolate planet with rich resources and inidginous life." +
                           "Lorem ipsum dolor sit amet, cu sed summo iudico fastidii." +
                           "Ut pri esse reque dicam, in nemore tincidunt eos. Eu clita" +
                           "periculis complectitur mel, docendi dolores ius no. Et est" +
                           "dicat noster. Et his vitae libris impetus, quem evertitur" +
                           "suscipiantur est at. Ea pertinax efficiendi pro, ubique" +
                           "delectus cum id. suscipiantur est at. Ea pertinax efficiendi " +
                           "pro, ubique" +
                           "Type - Terran" +
                           "Size - Large";

            var textCont = new WrappedTextContainer(longText, this._position, new Vector2f(10.0f, 10.0f), _width);

            var planetDataText = "Data of the planet";
            var planetData = new Text(planetDataText, GlobalAssets.FontNormal, 11);
            planetData.Color = Color.White;
            planetData.Position = new Vector2f(_position.X + (_width / 2.0f) - (w / 2.0f), _position.Y + 200.0f);

            var backRect = new RectangleShape(new Vector2f(_width, planetData.GetGlobalBounds().Height + 2));
            backRect.Position = new Vector2f(_position.X, _position.Y + 200.0f);
            backRect.FillColor = new Color(13, 41, 51, 255);

            target.Draw(backRect);
            target.Draw(planetData);
            target.Draw(textCont);
        }
    }
}
