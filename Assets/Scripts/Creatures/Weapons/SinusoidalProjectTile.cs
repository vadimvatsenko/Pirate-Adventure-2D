using UnityEngine;

// синусоидный ProjectTile
namespace Creatures.Weapons
{
    public class SinusoidalProjectTile : BaseProjectTile
    {
        private float _originalY;
        protected  void Start()
        {
            _originalY = Rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            Vector2 position = Rigidbody.position;
            position.x = FacingDirection * speed;
            position.y = _originalY + Mathf.Sin(Time.time);
            Rigidbody.MovePosition(position);
        }
    }
}