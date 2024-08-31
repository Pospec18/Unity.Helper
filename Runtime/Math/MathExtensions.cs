using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class MathExtensions
    {
        #region Vector Extensions

        public static Vector3 PlaneProjection(this Vector2 v) => new Vector3(v.x, 0, v.y);

        public static Vector3 ComponentwiseMultiply(this Vector3 v, Vector3 a) => new Vector3(v.x * a.x, v.y * a.y, v.z * a.z);

        public static Vector2 Mod(this Vector2 v, int m) => new Vector2(v.x % m, v.y % m);
        public static Vector2Int Mod(this Vector2Int v, int m) => new Vector2Int(v.x % m, v.y % m);
        public static Vector3 Mod(this Vector3 v, int m) => new Vector3(v.x % m, v.y % m, v.z % m);
        public static Vector3 Mod(this Vector3Int v, int m) => new Vector3Int(v.x % m, v.y % m, v.z % m);
        public static Vector3 Mod(this Vector4 v, int m) => new Vector4(v.x % m, v.y % m, v.z % m, v.w % m);
        public static Vector2 Abs(this Vector2 v) => new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));

        public static List<Vector2Int> GetNeighbours(this Vector2Int center, bool allowDiagonal = true)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>();
            neighbours.Add(center + Vector2Int.left);
            neighbours.Add(center + Vector2Int.right);
            neighbours.Add(center + Vector2Int.up);
            neighbours.Add(center + Vector2Int.down);

            if (allowDiagonal)
            {
                neighbours.Add(center + new Vector2Int(1, 1));
                neighbours.Add(center + new Vector2Int(1, -1));
                neighbours.Add(center + new Vector2Int(-1, 1));
                neighbours.Add(center + new Vector2Int(-1, -1));
            }

            return neighbours;
        }

        public static List<Vector2> GetNeighbours(this Vector2 center, bool allowDiagonal = true)
        {
            List<Vector2> neighbours = new List<Vector2>();
            neighbours.Add(center + Vector2.left);
            neighbours.Add(center + Vector2.right);
            neighbours.Add(center + Vector2.up);
            neighbours.Add(center + Vector2.down);

            if (allowDiagonal)
            {
                neighbours.Add(center + new Vector2(1, 1));
                neighbours.Add(center + new Vector2(1, -1));
                neighbours.Add(center + new Vector2(-1, 1));
                neighbours.Add(center + new Vector2(-1, -1));
            }

            return neighbours;
        }

        #endregion

        #region Numeric Extensions

        public static int Normalize(this int x)
        {
            if (x == 0)
                return 0;
            return x > 0 ? 1 : -1;
        }

        public static float Normalize(this float x)
        {
            if (x == 0)
                return 0;
            return x > 0 ? 1 : -1;
        }

        #endregion

        #region Bounds Extensions

        //public static void Cut(this ref Bounds bounds, Vector2 point)
        //{
        //    var max = bounds.max;
        //    var min = bounds.min;
        //    if (point.x > bounds.center.x)
        //    {
        //        if (max.x > point.x)
        //            max.x = point.x;
        //    }
        //    else
        //    {
        //        if (min.x < point.x)
        //            min.x = point.x;
        //    }

        //    if (point.y > bounds.center.y)
        //    {
        //        if (max.y > point.y)
        //            max.y = point.y;
        //    }
        //    else
        //    {
        //        if (min.y < point.y)
        //            min.y = point.y;
        //    }

        //    bounds.max = max;
        //    bounds.min = min;
        //}

        #endregion
    }
}
