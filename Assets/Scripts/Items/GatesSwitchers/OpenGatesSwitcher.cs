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

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void ActivatedPlatform()
        {
            if (isActivated) return;
            _rb.MovePosition(_rb.position + Vector2.down * yOffset);
            isActivated = true;
        }

        public void DiactivatedPlatform()
        {
            if (!isActivated) return;
            _rb.MovePosition(_rb.position + Vector2.up * yOffset);
            isActivated = false;
        }
    }
}