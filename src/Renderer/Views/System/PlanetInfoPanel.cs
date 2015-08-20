using System.Collections.Generic;
using Renderer.Controls.Base;
using Renderer.Controls.Panels;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views.System
{
    public class PlanetInfoPanel : IBaseControl
    {
        private readonly Vector2f _pos;
        private readonly StarSystem.Planet _planet;
        private readonly Sprite _planetSprite;
        private NamedPanel planetInfoPanel;

        public PlanetInfoPanel(Vector2f pos)
        {
            _pos = pos;
        }

        public void Update(StarSystem.Planet planet)
        {
            // TODO: generated based on the object
            planetInfoPanel = new NamedPanel(planet.Name, _pos, 200.0f, 400.0f);
            planetInfoPanel.AddChild(new WrappedTextContainer(planet.Description, new Vector2f(10.0f, 10.0f), planetInfoPanel));

            var newDataDict = new Dictionary<string, string>
            {
                { "Scanned", "50%" },
                { "Explored", "15%" },
                { "Type", planet.Type.ToString() },
                { "Size", planet.Size.ToString() },
                { "Mineral", "Rich" },
                { "Life Forms", "Many" }
            };

            planetInfoPanel.AddChild(new NamePlateContainer("Info", new Vector2f(10.0f, 200.0f), planetInfoPanel));
            //TODO: this text has to be child of the NamePlateContainer above
            planetInfoPanel.AddChild(new LeftRightAlignedText(newDataDict, new Vector2f(10.0f, 230.0f), planetInfoPanel));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (planetInfoPanel != null)
            {
                target.Draw(planetInfoPanel);
            }
        }

        public FloatRect GlobalBounds => planetInfoPanel.GlobalBounds;
    }
}
