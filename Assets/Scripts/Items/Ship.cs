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

        private Vector3 _startPos;
        private Rigidbody2D _rigidbody;
        private float _timeOffset;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
            _timeOffset = Random.Range(0f, 100f); // Чтобы не все качались одинаково
        }

        void Update()
        {
            float bob = Mathf.Sin((Time.time + _timeOffset) * frequency) * amplitude;
            float angle = Mathf.Sin((Time.time + _timeOffset) * rotationFrequency) * rotationAmplitude;

            _rigidbody.MovePosition(_startPos + Vector3.up * bob);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}