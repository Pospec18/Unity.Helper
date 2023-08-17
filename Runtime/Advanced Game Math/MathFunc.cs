using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class MathFunc
    {
        public static Vector2 Avg(params Vector2[] positions)
        {
            int count = 0;
            Vector2 sum = Vector2.zero;
            foreach (Vector2 pos in positions)
            {
                sum += pos;
                count++;
            }

            return sum / count;
        }

        public static float Avg(params float[] numbers)
        {
            int count = 0;
            float sum = 0;
            foreach (float num in numbers)
            {
                sum += num;
                count++;
            }

            return sum / count;
        }
    }
}
