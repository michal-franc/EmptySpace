using System;
using System.Runtime.InteropServices.ComTypes;
using SFML.Graphics;
using SFML.Window;

namespace Renderer.CustomShapes
{
    public static class Textures
    {
        public static Texture TexBlurred;
        public static Texture TexSolid;

        static Textures()
        {
            TexSolid = new Texture("gfx/stars/solid_yellow.png");
            TexBlurred = new Texture("gfx/stars/blur_yellow.png");
        }
    }

    public static class Rand
    {
        static Rand()
        {
            Random = new Random();
        }

        public static Random Random { get; set; }
    }

    public class StarSprite : Drawable
    {
        private Sprite _spriteSolid;
        private Sprite _spriteBlurred;
        private double animCounter;
        private double counter = 0;

        public StarSprite(StarSystem.StarSystem system, Vector2f position)
        {
            animCounter = Rand.Random.Next(0, 255);
            _spriteBlurred = new Sprite(Textures.TexBlurred);
            _spriteSolid = new Sprite(Textures.TexSolid);
            _spriteSolid.Position = position;
            _spriteSolid.Color = system.Sun.Color;
            _spriteBlurred.Position = position;
            _spriteBlurred.Color = system.Sun.Color;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            var eistC = _spriteBlurred.Color;
            eistC.A = (byte)animCounter;

            _spriteBlurred.Color = eistC;
            target.Draw(_spriteBlurred, new RenderStates(BlendMode.Add));
            target.Draw(_spriteSolid);

            animCounter += counter;
            if (animCounter >= 100)
            {
                counter = -counter;
                animCounter = 100;
            }

            if (animCounter <= 0)
            {
                counter = -counter;
                animCounter = 0;
            }
        }

        public FloatRect GetGlobalBounds()
        {
            return _spriteSolid.GetGlobalBounds();
        }
    }

    public class StarShape : CircleShape
    {
        public StarShape(StarSystem.StarSystem system, Vector2f position)
            :base(2.0f)
        {
            this.Position = position;
            this.FillColor = Color.White;
            this.OutlineColor = system.Sun.Color;
            this.OutlineThickness = 3;
        }
    }
}