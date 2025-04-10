using System;
using PlayerFolder;
using UnityEngine;

namespace Components
{
    // механика платформера, например если бочка упёрлась в стену мы можем ее сдвинуть если запрыгнуть на нее
    // и начать идти в сторону стены
    public class RollingItemsComponent :MonoBehaviour
    {
        [SerializeField] private float pushMultiplier = 1f;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (player == null) return;
            
            if (player.IsWallDetected && player.XInput != 0)
            {
                // Двигаем в противоположную сторону от взгляда игрока
                _rb.velocity = new Vector2(-player.FacingDirection * pushMultiplier, _rb.velocity.y);
            }
        }
    }
}