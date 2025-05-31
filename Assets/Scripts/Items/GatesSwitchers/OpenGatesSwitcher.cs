using System;
using PlayerFolder;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public class OpenGatesSwitcher : MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] float yOffset = 0.075f;
        [SerializeField] bool isActivated = false;
        [SerializeField] private UnityEvent onSwitch;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void EnterOnPlatform()
        {
            if (isActivated) return;
            
            _rb.MovePosition(_rb.position + Vector2.down * yOffset);
            isActivated = true;
            
            onSwitch?.Invoke();
        }

        public void ExitFromPlatform()
        {
            if (!isActivated) return;
            _rb.MovePosition(_rb.position + Vector2.up * yOffset);
            onSwitch?.Invoke();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player != null)
            {
                EnterOnPlatform();
            }
            else
            {
                ExitFromPlatform();
            }
        }
    }
}