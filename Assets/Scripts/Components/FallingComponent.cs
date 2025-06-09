using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class FallingComponent : MonoBehaviour
    {
        [SerializeField] private float minimalFallingSpeed = -2f;
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private UnityEvent onFall;
        private Rigidbody2D _rigidbody;
        private bool _isFalling = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_rigidbody.velocity.y > minimalFallingSpeed)
            {
                _isFalling = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.layer = LayerMask.NameToLayer("Ground");
            if (_isFalling && collision.gameObject.layer == collisionMask)
            {
                onFall?.Invoke();
            }
        }
    }
}