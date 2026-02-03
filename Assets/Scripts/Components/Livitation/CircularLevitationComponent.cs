using UnityEngine;

namespace Components.Livitation
{
    public class CircularLevitationComponent : BaseLevitationComponent
    {
        [SerializeField] private float radiusX = 1f;  
        [SerializeField] private float radiusY = 1f; 
        
        private Vector2 _center;

        protected override void Awake()
        {
            base.Awake();
            _center = Rigidbody2D.position;
        }

        private void FixedUpdate()
        {
            float angle = Seed + Time.time * frequency;

            Vector2 position = _center;
            position.x += Mathf.Cos(angle) * radiusX;
            position.y += Mathf.Sin(angle) * radiusY;

            Rigidbody2D.MovePosition(position);
        }

        
        // монету можно таскать, но нужно сбросить центр 
        public void ResetCenter()
        {
            _center = Rigidbody2D.position;
        }
    }
}