using SFML.Graphics;

namespace Renderer
{
    public static class GlobalAssets
    {
        public static Font Font { get; }
        public static Texture PlanetsSprite { get; }
        public static Sprite SpaceBackground { get; }

        static GlobalAssets()
        {
            var planetImage = new Image("gfx/planets.png");
            planetImage.CreateMaskFromColor(Color.Black);
            PlanetsSprite = new Texture(planetImage);

            SpaceBackground = new Sprite(new Texture("gfx/space_back.png"));
            Font = new Font("ariblk.ttf");
        }
    }
}
