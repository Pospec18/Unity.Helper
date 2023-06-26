using System;
using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class Norms
    {
        #region p-norm
        /// <summary>
        /// Universal norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="p">power factor (1 = Manhattan, 2 = Euclidean)</param>
        /// <returns>norm of the vector</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be higher than 0</exception>
        public static float PNorm(this Vector2 v, uint p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException("p", "Value must be grater than 0");
            return Mathf.Pow(Mathf.Pow(Mathf.Abs(v.x), p) + Mathf.Pow(Mathf.Abs(v.y), p), 1.0f / p);
        }

        /// <summary>
        /// Universal norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="p">power factor (1 = Manhattan, 2 = Euclidean)</param>
        /// <returns>norm of the vector</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be higher than 0</exception>
        public static float PNorm(this Vector2Int v, uint p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException("p", "Value must be grater than 0");
            return Mathf.Pow(Mathf.Pow(Mathf.Abs(v.x), p) + Mathf.Pow(Mathf.Abs(v.y), p), 1.0f / p);
        }

        /// <summary>
        /// Universal norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="p">power factor (1 = Manhattan, 2 = Euclidean)</param>
        /// <returns>norm of the vector</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be higher than 0</exception>
        public static float PNorm(this Vector3 v, uint p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException("p", "Value must be grater than 0");
            return Mathf.Pow(Mathf.Pow(Mathf.Abs(v.x), p) + Mathf.Pow(Mathf.Abs(v.y), p) + Mathf.Pow(Mathf.Abs(v.z), p), 1.0f / p);
        }

        /// <summary>
        /// Universal norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="p">power factor (1 = Manhattan, 2 = Euclidean)</param>
        /// <returns>norm of the vector</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be higher than 0</exception>
        public static float PNorm(this Vector3Int v, uint p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException("p", "Value must be grater than 0");
            return Mathf.Pow(Mathf.Pow(Mathf.Abs(v.x), p) + Mathf.Pow(Mathf.Abs(v.y), p) + Mathf.Pow(Mathf.Abs(v.z), p), 1.0f / p);
        }

        /// <summary>
        /// Universal norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <param name="p">power factor (1 = Manhattan, 2 = Euclidean)</param>
        /// <returns>norm of the vector</returns>
        /// <exception cref="ArgumentOutOfRangeException">p must be higher than 0</exception>
        public static float PNorm(this Vector4 v, uint p)
        {
            if (p < 1)
                throw new ArgumentOutOfRangeException("p", "Value must be grater than 0");
            return Mathf.Pow(Mathf.Pow(Mathf.Abs(v.x), p) + Mathf.Pow(Mathf.Abs(v.y), p) + Mathf.Pow(Mathf.Abs(v.z), p) + Mathf.Pow(Mathf.Abs(v.w), p), 1.0f / p);
        }
        #endregion

        #region manhattan norm
        /// <summary>
        /// Norm for square tiles games where you can't move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float ManhattanNorm(this Vector2 v)
        {
            return v.x + v.y;
        }
        
        /// <summary>
        /// Norm for square tiles games where you can't move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float ManhattanNorm(this Vector2Int v)
        {
            return v.x + v.y;
        }

        /// <summary>
        /// Norm for square tiles games where you can't move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float ManhattanNorm(this Vector3 v)
        {
            return v.x + v.y + v.z;
        }

        /// <summary>
        /// Norm for square tiles games where you can't move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float ManhattanNorm(this Vector3Int v)
        {
            return v.x + v.y + v.z;
        }

        /// <summary>
        /// Norm for square tiles games where you can't move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float ManhattanNorm(this Vector4 v)
        {
            return v.x + v.y + v.z + v.w;
        }
        #endregion

        #region maximum norm
        /// <summary>
        /// Norm for square tiles games with abillity to move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float MaximumNorm(this Vector2 v)
        {
            return Mathf.Max(v.x, v.y);
        }

        /// <summary>
        /// Norm for square tiles games with abillity to move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float MaximumNorm(this Vector2Int v)
        {
            return Mathf.Max(v.x, v.y);
        }

        /// <summary>
        /// Norm for square tiles games with abillity to move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float MaximumNorm(this Vector3 v)
        {
            return Mathf.Max(v.x, v.y, v.z);
        }

        /// <summary>
        /// Norm for square tiles games with abillity to move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float MaximumNorm(this Vector3Int v)
        {
            return Mathf.Max(v.x, v.y, v.z);
        }

        /// <summary>
        /// Norm for square tiles games with abillity to move diagonaly
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float MaximumNorm(this Vector4 v)
        {
            return Mathf.Max(v.x, v.y, v.z, v.w);
        }
        #endregion

        #region euclidean norm
        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float EuclideanNorm(this Vector2 v)
        {
            return Mathf.Sqrt(v.x * v.x + v.y * v.y);
        }

        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float EuclideanNorm(this Vector2Int v)
        {
            return Mathf.Sqrt(v.x * v.x + v.y * v.y);
        }

        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float EuclideanNorm(this Vector3 v)
        {
            return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }

        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float EuclideanNorm(this Vector3Int v)
        {
            return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        }

        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="v">vector</param>
        /// <returns>norm of the vector</returns>
        public static float EuclideanNorm(this Vector4 v)
        {
            return Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w);
        }

        /// <summary>
        /// Classical norm
        /// </summary>
        /// <param name="q">quaternion</param>
        /// <returns>norm of the quaternion</returns>
        public static float EuclideanNorm(this Quaternion q)
        {
            return Mathf.Sqrt(q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w);
        }
        #endregion
    }
}
