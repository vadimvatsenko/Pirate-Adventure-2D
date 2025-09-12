using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Items.Candles
{
    public class CandleFlicker : MonoBehaviour
    {
        [SerializeField] private float flickerSpeed = 1f;           // скорость "шума"
        [SerializeField] private float flickerStrength = 0.2f;       // амплитуда колебаний
        [SerializeField] private float baseIntensity = 1.2f;         // базовая яркость
        [SerializeField] private float baseInnerRadius = 0.3f;
        [SerializeField] private float baseOuterRadius = 1.0f;

        [SerializeField] private float radiusVariation = 0.1f;       // насколько радиус колеблется

        private Light2D _light;
        private float _timeOffset;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            _timeOffset = Random.Range(0f, 1000f); // чтобы каждый свет был уникален
        }

        private void Update()
        {
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, _timeOffset);

            // варьируем интенсивность
            _light.intensity = baseIntensity + (noise - 0.5f) * flickerStrength;

            // варьируем радиусы
            _light.pointLightInnerRadius = baseInnerRadius + (noise - 0.5f) * radiusVariation;
            _light.pointLightOuterRadius = baseOuterRadius + (noise - 0.5f) * radiusVariation * 2f;
        }
    }
}