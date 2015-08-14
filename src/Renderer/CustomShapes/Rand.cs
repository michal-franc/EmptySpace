using System;

namespace Renderer.CustomShapes
{
    public static class Rand
    {
        static Rand()
        {
            Random = new Random();
        }

        public static Random Random { get; set; }
    }
}