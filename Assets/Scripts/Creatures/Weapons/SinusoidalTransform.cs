using UnityEngine;

namespace Creatures.Weapons
{
    public class SinusoidalTransform : MonoBehaviour
    {
        [SerializeField] private float frequency = 1f;
        [SerializeField] private float amplitude = 1f;
        [SerializeField] private float speed = 5f;
        
        private Vector2 _tempPosition;
        
        private float _originalY;
        private float _originalX;
        private float _time;
        protected  void Start()
        {
            _originalY = transform.position.y;
            _originalX = transform.position.x;
        }

        private void FixedUpdate()
        {
            Vector2 _tempPosition = transform.position;
            _tempPosition.x += speed * Time.fixedDeltaTime;
            _tempPosition.y = _originalY + Mathf.Sin(_time * frequency) * amplitude;
            transform.position = _tempPosition;
            
            _time += Time.fixedDeltaTime;
        }
    }
}