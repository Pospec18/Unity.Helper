using UnityEngine;

namespace Pospec.Helper
{
    public static class RotationHelper
    {
        public static float AngleInRadians(this Vector2 v) => Mathf.Atan2(v.y, v.x);
        public static float AngleInRadians(this Vector2Int v) => Mathf.Atan2(v.y, v.x);

        public static void Rotate(this Vector2 v, float radians)
        {
            v.x = v.x * Mathf.Cos(radians) - v.y * Mathf.Sin(radians);
            v.y = v.x * Mathf.Sin(radians) + v.y * Mathf.Cos(radians);
        }

        public static Vector2 PointOnCircle(float radians, float radius)
        {
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * radius;
        }

        public static Vector3 PointOnSphere(float inclination, float azimuth, float radius)
        {
            return new Vector3(
                radius * Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                radius * Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                radius * Mathf.Cos(inclination));
        }

        public static Quaternion RotationOnSphere(float inclination, float azimuth, float up)
        {
            Vector3 point = PointOnSphere(inclination, azimuth, 1);
            return Quaternion.FromToRotation(Vector3.forward, point) * Quaternion.AngleAxis(up, Vector3.forward);
        }

    }
}
