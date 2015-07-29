using SFML.Graphics;
using SFML.Window;

namespace Renderer.NotUsed
{
    public class MapRenderer : Transformable, Drawable
    {
        private Texture tileset ;
        private VertexArray vertices;

        public MapRenderer()
        {
            vertices = new VertexArray(PrimitiveType.Quads);
        }

        public void Load(string fileName, Vector2u tileSize, int[,] tiles)
        {
            var heigth = (uint)tiles.GetLength(1);
            var width = (uint)tiles.GetLength(0);
            tileset = new Texture(fileName);
            vertices.Resize(width * heigth * 4);

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < heigth; ++j)
                {
                    // get the current tile number
                    int tileNumber = tiles[i, j];

                    int specialFlags = tileNumber / 10000;

                    if (specialFlags > 0)
                    {
                        tileNumber = tileNumber - ( specialFlags * 10000 );
                    }

                    // find its position in the tileset texture
                    long tu = tileNumber % (tileset.Size.X / tileSize.X);
                    long tv = tileNumber / (tileset.Size.X / tileSize.X);

                    // get a pointer to the current tile's quad
                    var index0 = (uint)((i + j * width)*4);

                    var quad0 = vertices[index0];
                    var quad1 = vertices[index0 + 1];
                    var quad2 = vertices[index0 + 2];
                    var quad3 = vertices[index0 + 3];

                    // define its 4 corners
                    quad0.Position = new Vector2f(i * tileSize.X, j * tileSize.Y);
                    quad1.Position = new Vector2f((i + 1) * tileSize.X, j * tileSize.Y);
                    quad2.Position = new Vector2f((i + 1) * tileSize.X, (j + 1) * tileSize.Y);
                    quad3.Position = new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y);

                    if (specialFlags == 1)
                    {
                        quad0.TexCoords = new Vector2f((tu + 1)*tileSize.X, tv*tileSize.Y);
                        quad1.TexCoords = new Vector2f((tu + 1)*tileSize.X, (tv + 1)*tileSize.Y);
                        quad2.TexCoords = new Vector2f(tu*tileSize.X, (tv + 1)*tileSize.Y);
                        quad3.TexCoords = new Vector2f(tu*tileSize.X, tv*tileSize.Y);
                    }
                    else if (specialFlags == 2)
                    {
                        quad3.TexCoords = new Vector2f((tu + 1)*tileSize.X, tv*tileSize.Y);
                        quad0.TexCoords = new Vector2f((tu + 1)*tileSize.X, (tv + 1)*tileSize.Y);
                        quad1.TexCoords = new Vector2f(tu*tileSize.X, (tv + 1)*tileSize.Y);
                        quad2.TexCoords = new Vector2f(tu*tileSize.X, tv*tileSize.Y);
                    }
                    else if (specialFlags == 3)
                    {
                        quad2.TexCoords = new Vector2f((tu + 1)*tileSize.X, tv*tileSize.Y);
                        quad3.TexCoords = new Vector2f((tu + 1)*tileSize.X, (tv + 1)*tileSize.Y);
                        quad0.TexCoords = new Vector2f(tu*tileSize.X, (tv + 1)*tileSize.Y);
                        quad1.TexCoords = new Vector2f(tu*tileSize.X, tv*tileSize.Y);
                    }
                    else
                    {
                        // define its 4 texture coordinates
                        quad0.TexCoords = new Vector2f(tu * tileSize.X, tv * tileSize.Y);
                        quad1.TexCoords = new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y);
                        quad2.TexCoords = new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y);
                        quad3.TexCoords = new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y);
                    }

                    vertices[index0] = quad0;
                    vertices[index0 + 1] = quad1;
                    vertices[index0 + 2] = quad2;
                    vertices[index0 + 3] = quad3;
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= this.Transform;
            states.Texture = this.tileset; 
            target.Draw(this.vertices, states);
        }
    }
}