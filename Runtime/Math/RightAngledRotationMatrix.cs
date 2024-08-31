using System;
using UnityEngine;

namespace Pospec.Helper.Math
{
    [Serializable]
    public struct RightAngledRotationMatrix : ISerializationCallbackReceiver
    {
        [SerializeField] private RightAngledRotation degrees;
        private int x, y, z, w;

        public static RightAngledRotationMatrix Rotation0 = new RightAngledRotationMatrix() { x = 1, y = 0, z = 0, w = 1 };
        public static RightAngledRotationMatrix Rotation90 = new RightAngledRotationMatrix() { x = 0, y = -1, z = 1, w = 0 };
        public static RightAngledRotationMatrix Rotation180 = new RightAngledRotationMatrix() { x = -1, y = 0, z = 0, w = -1 };
        public static RightAngledRotationMatrix Rotation270 = new RightAngledRotationMatrix() { x = 0, y = 1, z = -1, w = 0 };

        public void Rotate90Clockwise()
        {
            this *= Rotation270;
        }

        public void Rotate90CounterClockwise()
        {
            this *= Rotation90;
        }

        public override bool Equals(object obj)
        {
            return obj is RightAngledRotationMatrix matrix &&
                   x == matrix.x &&
                   y == matrix.y &&
                   z == matrix.z &&
                   w == matrix.w;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public void OnBeforeSerialize()
        {
            float angle = (this * Vector2Int.right).AngleInRadians() * Mathf.Rad2Deg;
            degrees = (RightAngledRotation)Mathf.RoundToInt(angle);
            if (degrees < 0)
                degrees += 360;
        }

        public void OnAfterDeserialize()
        {
            x = Mathf.RoundToInt(Mathf.Cos((int)degrees * Mathf.Deg2Rad));
            z = Mathf.RoundToInt(Mathf.Sin((int)degrees * Mathf.Deg2Rad));
            y = -z;
            w = x;
        }

        public static Vector2Int operator *(RightAngledRotationMatrix m, Vector2Int v)
        {
            return new Vector2Int(v.x * m.x + v.y * m.y, v.x * m.z + v.y * m.w);
        }

        public static Vector2 operator *(RightAngledRotationMatrix m, Vector2 v)
        {
            return new Vector2(v.x * m.x + v.y * m.y, v.x * m.z + v.y * m.w);
        }

        public static RightAngledRotationMatrix operator *(RightAngledRotationMatrix a, RightAngledRotationMatrix b)
        {
            return new RightAngledRotationMatrix
            {
                x = a.x * b.x + a.y * b.z,
                y = a.x * b.y + a.y * b.w,
                z = a.z * b.x + a.w * b.z,
                w = a.z * b.y + a.w * b.w
            };
        }

        public static bool operator ==(RightAngledRotationMatrix a, RightAngledRotationMatrix b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        }

        public static bool operator !=(RightAngledRotationMatrix a, RightAngledRotationMatrix b)
        {
            return !(a == b);
        }
    }

    enum RightAngledRotation { Rotate0 = 0, Rotate90 = 90, Rotate180 = 180, Rotate270 = 270 }
}
