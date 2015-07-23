using System;
using System.Collections;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace Renderer
{
    public class GalaxyView: IGameView
    {
        private Universe.Universe universe;
        private ICollection<Button> _buttons;

        public GalaxyView(Universe.Universe universe)
        {
            this.universe = universe;
            _buttons = new List<Button>();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var font = new Font("ariblk.ttf");

            target.Draw(new TopBar("Galaxy", target.Size.X, font));

            foreach (var starSystem in universe.Systems)
            {
                var rect = new Button(string.Empty, new Vector2f(starSystem.Position.X, starSystem.Position.Y), font, (s) => s.ChangeView(new SystemView(starSystem)));
                _buttons.Add(rect);
                target.Draw(rect);
            }
        }

        public GameState HandleEvents(RenderWindow mainWindow, GameState currentState)
        {
            var leftPressed = Mouse.IsButtonPressed(Mouse.Button.Left);
            var mPos = Mouse.GetPosition(mainWindow);

            foreach (var b in _buttons)
            {
                if (leftPressed)
                {
                    b.HandleClick(mPos, currentState);
                }
            };

            return currentState;
        }
    }


    public class SystemView : IGameView
    {
        private readonly StarSystem.StarSystem _system;
        private StarSystem.SystemDrawRenderer _planetsRenderer;
        private Texture _texture;
        private Sprite _backSprite;
        private SpriteWithHint.SpriteWithHint _selectedObject;
        private Font _font;
        private IGameView _selectedGameView;

        private ICollection<Button> _buttons;

        public SystemView(StarSystem.StarSystem system)
        {
            _system = system;
            _planetsRenderer = new StarSystem.SystemDrawRenderer("gfx/planets.png");
            _texture = new Texture("gfx/space_back.png");
            _font = new Font("ariblk.ttf");
            _backSprite = new Sprite(_texture);
            _planetsRenderer.Create(_system);
            _buttons = new List<Button>();

            _buttons.Add(new Button("Galaxy", new Vector2f(100.0f, 500.0f), _font, (s) => s.ChangeView(new GalaxyView(s.Universe))));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_backSprite);
            target.Draw(new TopBar(_system.Sun.Name, target.Size.X, _font));
            target.Draw(_planetsRenderer);

            foreach (var b in _buttons)
            {
                target.Draw(b); 
            }
        }

        public void PlanetSelected(RenderWindow mainWindow)
        {
            _selectedObject.DrawSelected(mainWindow);

            var button = new Button("Explore", new Vector2f(300.0f, 500.0f), _font, null);
            mainWindow.Draw(button);
        }

        public GameState HandleEvents(RenderWindow mainWindow, GameState currentState)
        {
            var leftPressed = Mouse.IsButtonPressed(Mouse.Button.Left);
            var mPos = Mouse.GetPosition(mainWindow);

            if (_selectedObject != null) { this.PlanetSelected(mainWindow); }

            foreach (var s in _planetsRenderer.Sprites)
            {
                if (s.MouseInSprite(mPos))
                {
                    s.DrawHint(mainWindow);

                    if (leftPressed)
                        _selectedObject = s;
                }
            }

            foreach (var b in _buttons)
            {
                if (leftPressed)
                {
                    b.HandleClick(mPos, currentState);
                }
            }

            return currentState;
        }
    }
}