using System;
using System.Collections.Generic;
using Renderer.Controls.Buttons;
using Renderer.Controls.Panels;
using Renderer.StateEvents;
using Renderer.Views.System;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public class SystemView : GameView
    {
        private readonly string _systemName;
        private StarSystem.Planet _selectedObject;
        private SystemRenderer.SystemRenderer _renderer;
        private PlanetInfoPanel _planetInfoPanel;
        private PlanetInfoPanel _selectedplanetInfoPanel;

        public override string Name => $"System - '{_systemName}'";

        public SystemView(StarSystem.StarSystem system)
        {
            _systemName = system.Sun.Name;
            _renderer = new SystemRenderer.SystemRenderer(GlobalAssets.PlanetsSprite);

            _planetInfoPanel = new PlanetInfoPanel(new Vector2f(500.0f, 100.0f));
            _selectedplanetInfoPanel = new PlanetInfoPanel(new Vector2f(700.0f, 100.0f));

            this.AddSprites(system.Sun);
        }

        private void AddSprites(StarSystem.Sun sun)
        {
            var gapSunPlanet = 30.0f;
            var sunSprite = _renderer.CreateSunSprite(sun);
            this.Add(new SpriteButton(sunSprite, sun.Name));

            //Planet
            var shiftP = ShiftX(sunSprite.GetGlobalBounds(), gapSunPlanet);
            foreach (var p in sun.Planets)
            {
                var pS = _renderer.CreatePlanetSprite(p, shiftP.Item1, shiftP.Item2);

                shiftP = new Tuple<float, float>(shiftP.Item1 + pS.GetGlobalBounds().Width + gapSunPlanet, shiftP.Item2);
                AddPlanetControls(p, pS);

                //Moon
                var shiftM = ShiftY(pS.GetGlobalBounds(), gapSunPlanet);
                foreach (var m in p.Moons)
                {
                    var mS = _renderer.CreateMoonSprite(m, shiftM.Item1, shiftM.Item2);
                    shiftM = new Tuple<float, float>(shiftM.Item1, shiftM.Item2 + mS.GetGlobalBounds().Height + gapSunPlanet);
                    //AddMoonControls(m, mS);
                }
            }

            var selectedPlanet = new NamedPanel(string.Empty, new Vector2f(700.0f, 500.0f), 200.0f, 30.0f);
            selectedPlanet.AddChild(new Button("Scan", new Vector2f(10.0f, 3.0f), selectedPlanet));
            selectedPlanet.AddChild(new Button("Explore", new Vector2f(70.0f, 3.0f), selectedPlanet));
            
            this.Add(() => selectedPlanet, () => _selectedObject != null);
            this.Add(() => _selectedplanetInfoPanel, () => _selectedObject != null);
        }

        private void AddPlanetControls(StarSystem.Planet planet, Sprite planetSprite)
        {
            var planetS = new SpriteButton(planetSprite, planet.Name);
            planetS.OnHover += (sender, args) =>
            {
                _planetInfoPanel.Update(planet);
                sender.Draw(_planetInfoPanel);
            };

            planetS.OnLeftClick += (sender, state) =>
            {
                _selectedObject = planet;
                _selectedplanetInfoPanel.Update(planet);
                return new NoStateChange("PlanetClick", "Player has clicked planet");
            };

            this.Add(planetS);
        }

        //private void AddMoonControls(StarSystem.Moon moon, Sprite moonSprite)
        //{
        //    var moonS = new SpriteButton(moonSprite, moon.mName);
        //    // TODO: generated based on the object

        //    var newDataDict = new Dictionary<string, string>
        //    {
        //        { "Scanned", "50%" },
        //        { "Explored", "15%" },
        //        { "Type", moon.Type.ToString() },
        //        { "Size", moon.Size.ToString() },
        //        { "Mineral", "Rich" },
        //        { "Life Forms", "Many" }
        //    };
        //    var planetInfoPanel = new NamedPanel(moon.mName, new Vector2f(700.0f, 100.0f), 200.0f, 400.0f);
        //    planetInfoPanel.AddChild(new WrappedTextContainer(moon.Description, new Vector2f(10.0f, 10.0f), planetInfoPanel));
        //    planetInfoPanel.AddChild(new NamePlateContainer("Info", new Vector2f(10.0f, 200.0f), planetInfoPanel));
        //    //TODO: this text has to be child of the NamePlateContainer above
        //    planetInfoPanel.AddChild(new LeftRightAlignedText(newDataDict, new Vector2f(10.0f, 230.0f), planetInfoPanel));

        //    moonS.OnHover += (sender, args) =>
        //    {
        //        if (_selectedObject == null)
        //        {
        //            sender.Draw(planetInfoPanel);
        //        };
        //    };

        //    moonS.OnLeftClick += (sender, state) =>
        //    {
        //        _selectedObject = moonS;
        //        return new NoStateChange("PlanetClick", "Player has clicked planet");
        //    };
        //    //this.Add(() => planetInfoPanel, () => _selectedObject != null);
        //    this.Add(moonS);
        //}

        private Tuple<float,float> ShiftX(FloatRect rect, float move)
        {
            var cent = VectorHelper.centerPos(rect);
            return VectorHelper.addPos(cent.Item1, cent.Item2, (rect.Width / 2.0f) + move, 0.0f);
        }

        private Tuple<float, float> ShiftY(FloatRect rect, float move)
        {
            var cent = VectorHelper.centerPos(rect);
            return VectorHelper.addPos(cent.Item1, cent.Item2, 0.0f, (rect.Width / 2.0f) + move);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GlobalAssets.SpaceBackground);
            base.Draw(target,states);
        }
    }
}