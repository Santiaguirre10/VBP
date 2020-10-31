using UnityEngine;

namespace Gameplay
{
    public static class ParableFunction 
    {
        public static Vector2 Parable(float t, Vector2 a, Vector2 b, Vector2 c)
        {
            var ab = Vector2.Lerp(a, b, t);
            var bc = Vector2.Lerp(b, c, t);
            return Vector2.Lerp(ab, bc, t);
        }
    }
}
