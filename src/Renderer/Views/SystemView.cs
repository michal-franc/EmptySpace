using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Linq;
using Renderer.Controls;
using Renderer.Controls.Buttons;
using Renderer.Controls.Panels;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public class SystemView : GameView
    {
        private readonly string _systemName;
        private SpriteButton _selectedPlanet;

        public override string Name => $"System - '{_systemName}'";

        public SystemView(StarSystem.StarSystem system)
        {
            _systemName = system.Sun.Name;

            var planetsRenderer = new SystemRenderer.SystemRenderer(GlobalAssets.PlanetsSprite);
            var sprites = planetsRenderer.Create(system);

            foreach (var sprite in sprites)
            {
                var testDataOfPlanet = new Dictionary<string, string>
                {
                    { "Type", "Terran" },
                    { "Size", "Large" },
                    { "Something", "100" },
                    { "Something1", "150" },
                    { "Something2", "98.91" },
                    { "Something3", "1000" }
                };

                var planetInfoPanel = new NamedPanel("Planet Info", new Vector2f(700.0f, 100.0f), 200.0f, 400.0f);
                planetInfoPanel.AddChild(new WrappedTextContainer(sprite.Description, new Vector2f(10.0f, 10.0f), planetInfoPanel));
                planetInfoPanel.AddChild(new NamePlateContainer("Data", new Vector2f(10.0f, 200.0f), planetInfoPanel));
                //TODO: this text has to be child of the NamePlateContainer above
                planetInfoPanel.AddChild(new LeftRightAlignedText(testDataOfPlanet, new Vector2f(10.0f, 230.0f), planetInfoPanel));

                var planetSprite = new SpriteButton(sprite.Sprite, sprite.Name);
                planetSprite.OnClick += (sender, state) =>
                {
                    _selectedPlanet = planetSprite;
                    return state;
                };

                planetSprite.OnHover += (sender, args) =>
                {
                    sender.Draw(planetInfoPanel);
                };
                base.Add(planetSprite);
            }

            // TODO: change this logic completely, current solution is fine for static elements not for dynamically created ones
            // dynamic one is not yet ready to display here and is generated on some external action
            // static elements are fine with this code
            // this.Add(_selectePlanetInfoPanel, () => _selectedPlanet != null);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GlobalAssets.SpaceBackground);
            base.Draw(target,states);

            _selectedPlanet?.DrawSelected(target);
        }
    }
}