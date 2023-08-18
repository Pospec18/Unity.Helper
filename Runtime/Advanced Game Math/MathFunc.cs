using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class MathFunc
    {
        #region Sum

        public static int Sum(params int[] numbers)
        {
            int sum = default;
            foreach (int num in numbers)
                sum += num;

            return sum;
        }

        public static float Sum(params float[] numbers)
        {
            float sum = default;
            foreach (float num in numbers)
                sum += num;

            return sum;
        }

        public static Vector2 Sum(params Vector2[] positions)
        {
            Vector2 sum = default;
            foreach (Vector2 pos in positions)
                sum += pos;

            return sum;
        }

        public static Vector2Int Sum(params Vector2Int[] positions)
        {
            Vector2Int sum = default;
            foreach (Vector2Int pos in positions)
                sum += pos;

            return sum;
        }

        public static Vector3 Sum(params Vector3[] positions)
        {
            Vector3 sum = default;
            foreach (Vector3 pos in positions)
                sum += pos;

            return sum;
        }

        public static Vector3Int Sum(params Vector3Int[] positions)
        {
            Vector3Int sum = default;
            foreach (Vector3Int pos in positions)
                sum += pos;

            return sum;
        }

        public static Vector4 Sum(params Vector4[] positions)
        {
            Vector4 sum = default;
            foreach (Vector4 pos in positions)
                sum += pos;

            return sum;
        }

        #endregion

        #region Average

        public static float Avg(params float[] numbers)
        {
            return Sum(numbers) / numbers.Length;
        }

        public static Vector2 Avg(params Vector2[] positions)
        {
            return Sum(positions) / positions.Length;
        }

        public static Vector3 Avg(params Vector3[] positions)
        {
            return Sum(positions) / positions.Length;
        }

        public static Vector4 Avg(params Vector4[] positions)
        {
            return Sum(positions) / positions.Length;
        }

        #endregion

        #region Min Max

        public static int Min(params int[] numbers)
        {
            int min = int.MaxValue;
            foreach (var num in numbers)
                if (num < min)
                    min = num;

            return min;
        }

        public static float Min(params float[] numbers)
        {
            float min = float.MaxValue;
            foreach (var num in numbers)
                if (num < min)
                    min = num;

            return min;
        }


        public static int Max(params int[] numbers)
        {
            int max = int.MinValue;
            foreach (var num in numbers)
                if (num > max)
                    max = num;

            return max;
        }

        public static float Max(params float[] numbers)
        {
            float max = float.MinValue;
            foreach (var num in numbers)
                if (num > max)
                    max = num;

            return max;
        }

        public static Vector2 Leftest(params Vector2[] positions)
        {
            Vector2 leftest = new Vector2(float.MaxValue, 0);
            foreach (var pos in positions)
                if (pos.x < leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector2Int Leftest(params Vector2Int[] positions)
        {
            Vector2Int leftest = new Vector2Int(int.MaxValue, 0);
            foreach (var pos in positions)
                if (pos.x < leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector3 Leftest(params Vector3[] positions)
        {
            Vector3 leftest = new Vector3(float.MaxValue, 0);
            foreach (var pos in positions)
                if (pos.x < leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector3Int Leftest(params Vector3Int[] positions)
        {
            Vector3Int leftest = new Vector3Int(int.MaxValue, 0);
            foreach (var pos in positions)
                if (pos.x < leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector4 Leftest(params Vector4[] positions)
        {
            Vector4 leftest = new Vector4(float.MaxValue, 0);
            foreach (var pos in positions)
                if (pos.x < leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector2 Rightest(params Vector2[] positions)
        {
            Vector2 leftest = new Vector2(float.MinValue, 0);
            foreach (var pos in positions)
                if (pos.x > leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector2Int Rightest(params Vector2Int[] positions)
        {
            Vector2Int leftest = new Vector2Int(int.MinValue, 0);
            foreach (var pos in positions)
                if (pos.x > leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector3 Rightest(params Vector3[] positions)
        {
            Vector3 leftest = new Vector3(float.MinValue, 0);
            foreach (var pos in positions)
                if (pos.x > leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector3Int Rightest(params Vector3Int[] positions)
        {
            Vector3Int leftest = new Vector3Int(int.MinValue, 0);
            foreach (var pos in positions)
                if (pos.x > leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector4 Rightest(params Vector4[] positions)
        {
            Vector4 leftest = new Vector4(float.MinValue, 0);
            foreach (var pos in positions)
                if (pos.x > leftest.x)
                    leftest = pos;

            return leftest;
        }

        public static Vector2 Lowest(params Vector2[] positions)
        {
            Vector2 leftest = new Vector2(0, float.MaxValue);
            foreach (var pos in positions)
                if (pos.y < leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector2Int Lowest(params Vector2Int[] positions)
        {
            Vector2Int leftest = new Vector2Int(0, int.MaxValue);
            foreach (var pos in positions)
                if (pos.y < leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector3 Lowest(params Vector3[] positions)
        {
            Vector3 leftest = new Vector3(0, float.MaxValue);
            foreach (var pos in positions)
                if (pos.y < leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector3Int Lowest(params Vector3Int[] positions)
        {
            Vector3Int leftest = new Vector3Int(0, int.MaxValue);
            foreach (var pos in positions)
                if (pos.y < leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector4 Lowest(params Vector4[] positions)
        {
            Vector4 leftest = new Vector4(0, float.MaxValue);
            foreach (var pos in positions)
                if (pos.y < leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector2 Highest(params Vector2[] positions)
        {
            Vector2 leftest = new Vector2(0, float.MinValue);
            foreach (var pos in positions)
                if (pos.y > leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector2Int Highest(params Vector2Int[] positions)
        {
            Vector2Int leftest = new Vector2Int(0, int.MinValue);
            foreach (var pos in positions)
                if (pos.y > leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector3 Highest(params Vector3[] positions)
        {
            Vector3 leftest = new Vector3(0, float.MinValue);
            foreach (var pos in positions)
                if (pos.y > leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector3Int Highest(params Vector3Int[] positions)
        {
            Vector3Int leftest = new Vector3Int(0, int.MinValue);
            foreach (var pos in positions)
                if (pos.y > leftest.y)
                    leftest = pos;

            return leftest;
        }

        public static Vector4 Highest(params Vector4[] positions)
        {
            Vector4 leftest = new Vector4(0, float.MinValue);
            foreach (var pos in positions)
                if (pos.y > leftest.y)
                    leftest = pos;

            return leftest;
        }

        #endregion
    }
}
