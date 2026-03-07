using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Items.Candles
{
    [RequireComponent(typeof(Light2D))]
    public class CandleFlicker : MonoBehaviour
    {
        [Header("Flicker")]
        [SerializeField] private float flickerSpeed = 1f;
        [SerializeField] private float flickerStrength = 0.2f;
        [SerializeField] private float baseIntensity = 1.2f;
        [SerializeField] private float baseInnerRadius = 0.3f;
        [SerializeField] private float baseOuterRadius = 1.0f;
        [SerializeField] private float radiusVariation = 0.1f;

        [Header("Circular motion (light only)")]
        [SerializeField] private float frequency = 1f;   // обороты в секунду
        [SerializeField] private float radius = 0.05f;   // радиус круга (локально, в юнитах)
        [SerializeField] private bool randomizePhase = true;

        private Light2D _light;
        private float _seed;
        private float _timeOffset;

        private Vector3 _baseLocalPos;

        private void Awake()
        {
            _light = GetComponent<Light2D>();

            _baseLocalPos = transform.localPosition; // базовая локальная позиция света
            _seed = randomizePhase ? Random.value * Mathf.PI * 2f : 0f;
            _timeOffset = Random.Range(0f, 1000f);
        }

        private void Update()
        {
            // --- flicker ---
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, _timeOffset);

            _light.intensity = baseIntensity + (noise - 0.5f) * flickerStrength;
            _light.pointLightInnerRadius = baseInnerRadius + (noise - 0.5f) * radiusVariation;
            _light.pointLightOuterRadius = baseOuterRadius + (noise - 0.5f) * radiusVariation * 2f;

            // --- circular motion ---
            float angle = _seed + Time.time * (Mathf.PI * 2f * frequency);
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;

            transform.localPosition = _baseLocalPos + offset;
        }

        private void OnDisable()
        {
            // чтобы при выключении компонента свет не оставался "сдвинутым"
            transform.localPosition = _baseLocalPos;
        }
    }
}