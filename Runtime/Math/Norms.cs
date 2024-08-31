using System;
using UnityEngine;

namespace Pospec.Helper.Math
{
    /// <summary>
    /// Interface for computing length of vectors
    /// </summary>
    public interface INorm
    {
        public float Length(Vector2 v);
        public float Length(Vector2Int v);
        public float Length(Vector3 v);
        public float Length(Vector3Int v);
        public float Length(Vector4 v);
    }

    /// <summary>
    /// Computing vector length for normal (non-tile) worlds
    /// </summary>
    public class EuclideanNorm : INorm
    {
        public float Length(Vector2 v) => Mathf.Sqrt(v.x * v.x + v.y * v.y);
        public float Length(Vector2Int v) => Mathf.Sqrt(v.x * v.x + v.y * v.y);
        public float Length(Vector3 v) => Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        public float Length(Vector3Int v) => Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        public float Length(Vector4 v) => Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w);
    }

    /// <summary>
    /// Computing vector length for square-tile games where you can't move diagonaly
    /// </summary>
    public class ManhattanNorm : INorm
    {
        public float Length(Vector2 v) => Mathf.Abs(v.x) + Mathf.Abs(v.y);
        public float Length(Vector2Int v) => Mathf.Abs(v.x) + Mathf.Abs(v.y);
        public float Length(Vector3 v) => Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z);
        public float Length(Vector3Int v) => Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z);
        public float Length(Vector4 v) => Mathf.Abs(v.x) + Mathf.Abs(v.y) + Mathf.Abs(v.z) + Mathf.Abs(v.w);
    }

    /// <summary>
    /// Computing vector length for square-tile games with abillity to move diagonaly
    /// </summary>
    public class MaximumNorm : INorm
    {
        public float Length(Vector2 v) => Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
        public float Length(Vector2Int v) => Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));
        public float Length(Vector3 v) => Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        public float Length(Vector3Int v) => Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        public float Length(Vector4 v) => Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z), Mathf.Abs(v.w));
    }

    /// <summary>
    /// Universal norm (1 = Manhattan, 2 = Euclidean)
    /// </summary>
    public class PNorm : INorm
    {
        private readonly float p;

        public PNorm(float p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException(nameof(p), "Value must be grater than 0");
            this.p = p;
        }

        private float Pow(float x) => Mathf.Pow(Mathf.Abs(x), p);

        public float Length(Vector2 v) => Mathf.Pow(Pow(v.x) + Pow(v.y), 1.0f / p);
        public float Length(Vector2Int v) => Mathf.Pow(Pow(v.x) + Pow(v.y), 1.0f / p);
        public float Length(Vector3 v) => Mathf.Pow(Pow(v.x) + Pow(v.y) + Pow(v.z), 1.0f / p);
        public float Length(Vector3Int v) => Mathf.Pow(Pow(v.x) + Pow(v.y) + Pow(v.z), 1.0f / p);
        public float Length(Vector4 v) => Mathf.Pow(Pow(v.x) + Pow(v.y) + Pow(v.z) + Pow(v.w), 1.0f / p);
    }

    /// <summary>
    /// Computing aproximate vector length for normal (non-tile) worlds
    /// Test if it's actualy faster than EuclideanNorm
    /// </summary>
    public class FastEuclideanNorm : INorm
    {
        // This is fairly tricky and complex process. For details, see https://en.wikipedia.org/wiki/Fast_inverse_square_root
        // From https://www.geeksforgeeks.org/fast-method-calculate-inverse-square-root-floating-point-number-ieee-754-format/
        private float FastSqrt(float x)
        {
            float xhalf = 0.5f * x;
            int i = BitConverter.ToInt32(BitConverter.GetBytes(x), 0);
            i = 0x5f3759d5 - (i >> 1);
            float invSqrt = BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
            invSqrt = invSqrt * (1.5f - xhalf * invSqrt * invSqrt);
            return x * invSqrt;
        }

        public float Length(Vector2 v) => FastSqrt(v.x * v.x + v.y * v.y);
        public float Length(Vector2Int v) => FastSqrt(v.x * v.x + v.y * v.y);
        public float Length(Vector3 v) => FastSqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        public float Length(Vector3Int v) => FastSqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        public float Length(Vector4 v) => Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w);
    }

    public static class Norms
    {
        public readonly static MaximumNorm ChessLike = new MaximumNorm();
        public readonly static ManhattanNorm Manhattan = new ManhattanNorm();
        public readonly static EuclideanNorm Normal = new EuclideanNorm();
    }
}
