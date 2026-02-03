using UnityEngine;

// синусоидный ProjectTile
namespace Creatures.Weapons
{
    public class SinusoidalProjectTile : BaseProjectTile
    {
        [SerializeField] private float frequency = 1f;
        [SerializeField] private float amplitude = 1f;
        
        private float _originalY;
        private float _time;
        protected  void Start()
        {
            _originalY = Rigidbody.position.y;
        }

        private void FixedUpdate()
        {
            Vector2 position = Rigidbody.position;
            position.x += FacingDirection * speed;
            position.y = _originalY + Mathf.Sin(_time * frequency) * amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}