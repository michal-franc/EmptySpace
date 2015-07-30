using Renderer.Controls;
using Renderer.Controls.Buttons;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.Views
{
    public class SystemView : GameView
    {
        private string _systemName;
        private SpriteButton _selectedPlanet;

        public override string Name => $"System - '{_systemName}'";

        public SystemView(StarSystem.StarSystem system)
        {
            _systemName = system.Sun.Name;

            var planetsRenderer = new SystemRenderer.SystemRenderer(GlobalAssets.PlanetsSprite);
            planetsRenderer.Create(system);

            foreach (var sprite in planetsRenderer.Sprites)
            {
                var planetSprite = new SpriteButton(sprite.Sprite, sprite.Text);
                planetSprite.OnClick += (sender, state) =>
                {
                    _selectedPlanet = planetSprite;
                    return state;
                };

                planetSprite.OnHover += (sender, args) => sender.Draw(new Menu(new Vector2f(700.0f, 100.0f), 200.0f, 400.0f ));
                base.Add(planetSprite);
            }

            this.Add(new Menu(new Vector2f(700.0f, 100.0f), 200.0f, 400.0f), () => _selectedPlanet != null);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(GlobalAssets.SpaceBackground);
            base.Draw(target,states);

            _selectedPlanet?.DrawSelected(target);
        }
    }
}