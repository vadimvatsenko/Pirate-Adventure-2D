using UnityEngine;

namespace Components.Levitation
{
    public class VerticalLevitationMovementComponent : BaseLevitationComponent
    {
        private void FixedUpdate()
        {
            Vector2 position = Rigidbody2D.position;
            position.y = OriginalY + Mathf.Sin(Seed + Time.time * frequency) * amplitude;
            Rigidbody2D.MovePosition(position);
        }
    }
}