using System;
using SFML.Window;

namespace Renderer
{
    public static class Vector2FExtensions
    {
        public static Vector2f Apply(this Vector2f vect, Func<float, float> apply)
        {
            vect.X = apply(vect.X);
            vect.Y = apply(vect.Y);
            return vect;
        }

        public static Vector2f Normalize(this Vector2f vect)
        {
            Func<float, float> abs = (val) =>
            {
                var wasNegative = val > 0.0f;
                val = (float) Math.Sqrt(Math.Abs(val));
                return wasNegative ? val : -val;
            };

            return vect.Apply(abs);
        }
    }
}