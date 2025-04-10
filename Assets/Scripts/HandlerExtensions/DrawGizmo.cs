using UnityEngine;

namespace HandlerExtensions
{
    public static class DrawGizmo
    {
        public static void DrawGizmoBox(Vector2 start, Vector2 end, Vector2 size)
        {
            Vector2 halfSize = size * 0.5f;

            // 4 угла начала
            Vector3 topLeft = start + new Vector2(-halfSize.x, halfSize.y);
            Vector3 topRight = start + new Vector2(halfSize.x, halfSize.y);
            Vector3 bottomLeft = start + new Vector2(-halfSize.x, -halfSize.y);
            Vector3 bottomRight = start + new Vector2(halfSize.x, -halfSize.y);

            // 4 угла конца
            Vector3 endTopLeft = topLeft + (Vector3)(end - start);
            Vector3 endTopRight = topRight + (Vector3)(end - start);
            Vector3 endBottomLeft = bottomLeft + (Vector3)(end - start);
            Vector3 endBottomRight = bottomRight + (Vector3)(end - start);

            // Перед
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);

            // Зад
            Gizmos.DrawLine(endTopLeft, endTopRight);
            Gizmos.DrawLine(endTopRight, endBottomRight);
            Gizmos.DrawLine(endBottomRight, endBottomLeft);
            Gizmos.DrawLine(endBottomLeft, endTopLeft);

            // Связки
            Gizmos.DrawLine(topLeft, endTopLeft);
            Gizmos.DrawLine(topRight, endTopRight);
            Gizmos.DrawLine(bottomLeft, endBottomLeft);
            Gizmos.DrawLine(bottomRight, endBottomRight);
        }
    }
}