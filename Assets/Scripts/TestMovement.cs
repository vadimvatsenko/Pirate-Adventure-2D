using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class TestMovement : MonoBehaviour
    {
        
        private float _movementSpeed = 40f;
        private Vector2 direction = Vector2.right;
        private Rigidbody2D rb;
        private Collider2D collider;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            
        }

        private void Update()
        {
            rb.velocity = new Vector2(direction.x * _movementSpeed * Time.deltaTime, rb.velocity.y);
            
            Debug.Log(rb.velocity.y);
        }

        public void JumpItem()
        {
            rb.AddForce(Vector2.up * _movementSpeed, ForceMode2D.Impulse);
        }

    }
}