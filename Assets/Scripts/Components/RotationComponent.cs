using System;
using UnityEngine;

namespace Components
{
    public class RotationComponent : MonoBehaviour
    {
        [SerializeField] private float speedX = 180f; 
        private Rigidbody2D _rigidbody2D;

        public void Awake()
        {
            _rigidbody2D =  GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            transform.Rotate(speedX * Time.deltaTime, 0f, 0f, Space.Self);
        }
    }
}