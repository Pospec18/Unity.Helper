using UnityEngine;

namespace Pospec.Helper
{
    public static class DebugHelper
    {
        public static void DrawBox(Vector2 center, Vector2 size)
        {
            DrawBox(center, size, Color.white);
        }

        public static void DrawBox(Vector2 center, Vector2 size, Color color)
        {
            if (size.x < 0 || size.y < 0)
                Debug.Log($"Invalid {nameof(size)} {size}");
            Debug.DrawLine(center + new Vector2(size.x, size.y) / 2f, center + new Vector2(-size.x, size.y) / 2f, color);
            Debug.DrawLine(center + new Vector2(-size.x, size.y) / 2f, center + new Vector2(-size.x, -size.y) / 2f, color);
            Debug.DrawLine(center + new Vector2(-size.x, -size.y) / 2f, center + new Vector2(size.x, -size.y) / 2f, color);
            Debug.DrawLine(center + new Vector2(size.x, -size.y) / 2f, center + new Vector2(size.x, size.y) / 2f, color);
        }

        public static void DrawBox(Vector3 center, Vector2 size, float angle, Color color)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Debug.DrawLine(center + rotation * new Vector2(size.x, size.y) / 2f, center + rotation * new Vector2(-size.x, size.y) / 2f, color);
            Debug.DrawLine(center + rotation * new Vector2(-size.x, size.y) / 2f, center + rotation * new Vector2(-size.x, -size.y) / 2f, color);
            Debug.DrawLine(center + rotation * new Vector2(-size.x, -size.y) / 2f, center + rotation * new Vector2(size.x, -size.y) / 2f, color);
            Debug.DrawLine(center + rotation * new Vector2(size.x, -size.y) / 2f, center + rotation * new Vector2(size.x, size.y) / 2f, color);
        }
    }
}
