using UnityEngine;

namespace Pospec.Helper.Math
{
    public static class Transforms
    {
        public static Matrix4x4 AlignObject(Vector3 position, Vector3 front, Vector3 up)
        {
            Vector3 z = -Vector3.Normalize(front);
            if (z == Vector3.zero)
                z.Set(0, 1, 0);

            Vector3 x = Vector3.Normalize(Vector3.Cross(up, z));
            if (x == Vector3.zero)
                x.Set(0, 1, 0);

            Vector3 y = Vector3.Cross(z, x);
            return new Matrix4x4((Vector4)x, (Vector4)y, (Vector4)z, new Vector4(position.x, position.y, position.z, 1));
        }
    }
}