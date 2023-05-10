using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class Geometry
    {
        /// <summary>
        /// Calculates point on the circle
        /// </summary>
        /// <param name="angle">angle in degrees</param>
        /// <param name="radius">radius of the circle</param>
        /// <returns>point on the circle</returns>
        public static Vector2 PointOnCircle(float angle, float radius)
        {
            angle *= Mathf.PI / 180;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }
    }
}
