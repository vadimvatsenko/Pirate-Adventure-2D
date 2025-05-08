using UnityEngine;

namespace Items
{
    public class Ship : MonoBehaviour
    {
        [Header("Vertical Bobbing")]
        public float amplitude = 0.2f;      // Высота качания
        public float frequency = 1f;        // Скорость качания

        [Header("Rotation Bobbing")]
        public float rotationAmplitude = 2f; // Амплитуда поворота в градусах
        public float rotationFrequency = 0.5f; // Скорость поворота

        private Vector3 startPos;
        private Rigidbody2D _rigidbody;
        private float timeOffset;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            startPos = transform.position;
            timeOffset = Random.Range(0f, 100f); // Чтобы не все качались одинаково
        }

        void Update()
        {
            float bob = Mathf.Sin((Time.time + timeOffset) * frequency) * amplitude;
            float angle = Mathf.Sin((Time.time + timeOffset) * rotationFrequency) * rotationAmplitude;

            _rigidbody.MovePosition(startPos + Vector3.up * bob);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}