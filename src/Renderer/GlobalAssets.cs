using SFML.Graphics;

namespace Renderer
{
    public static class GlobalAssets
    {
        public static Font FontNormal { get; }
        public static Font FontBold { get; }
        public static Font FontLight { get; }
        public static Texture PlanetsSprite { get; }
        public static Sprite SpaceBackground { get; }

        static GlobalAssets()
        {
            var planetImage = new Image("gfx/planets.png");
            planetImage.CreateMaskFromColor(Color.Black);
            PlanetsSprite = new Texture(planetImage);

            SpaceBackground = new Sprite(new Texture("gfx/space_back.png"));
            FontNormal = new Font("orbitron-black.ttf");
            FontBold = new Font("orbitron-bold.ttf");
            FontLight = new Font("orbitron-light.ttf");
        }
    }
}
