using SFML.Graphics;

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
}