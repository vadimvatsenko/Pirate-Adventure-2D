using UnityEngine;

namespace HandlerExtensions
{
    public static class CollisionUtils
    {
        
        /// Проверяет, есть ли контакт с поверхностью "снизу".
        public static bool IsGroundedCollision(this Transform transform, Collision2D collision, float minDotThreshold = 0.9f)
        {
            /*foreach (var contact in collision.contacts)
            {
                // Если нормаль указывает почти вверх — значит, это "земля"
                if (Vector2.Dot(contact.normal, Vector2.up) >= minDotThreshold)
                    return true;
            }

            return false;*/
            
            Vector2 direction = collision.transform.position - transform.position;
            
            return Vector2.Dot(direction.normalized, Vector2.up) > minDotThreshold;
        }
        
        public static bool IsWallCollision(Collision2D collision, float minDotThreshold = 0.9f)
        {
            foreach (var contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                // Проверяем по X — если влево или вправо
                float dotRight = Vector2.Dot(normal, Vector2.right);
                float dotLeft = Vector2.Dot(normal, Vector2.left);

                if (dotRight >= minDotThreshold || dotLeft >= minDotThreshold)
                    return true;
            }

            return false;
        }
    }
}