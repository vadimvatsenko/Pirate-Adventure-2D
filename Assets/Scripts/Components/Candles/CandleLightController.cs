using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Components
{
    public class CandleLightController : MonoBehaviour
    {
        [SerializeField] private float targetInnerRadius = 0.5f;
        [SerializeField] private float targetOuterRadius = 1.5f;
        [SerializeField] private float duration = 1f;

        private Light2D _light;
        private float _innerRadius;
        private float _outerRadius;

        private void Awake()
        {
            _light = GetComponent<Light2D>();
            if (_light != null)
            {
                _innerRadius = _light.pointLightInnerRadius;
                _outerRadius = _light.pointLightOuterRadius;
            }
        }

        private void Start() 
        {
            if (_light != null) StartCoroutine(PulseLight());
        }

        private IEnumerator PulseLight()
        {
            while (true)
            {
                yield return StartCoroutine(LerpLight(
                    _light.pointLightInnerRadius,
                    _light.pointLightOuterRadius,
                    targetInnerRadius,
                    targetOuterRadius
                ));
                
                // ждет окончания метода, затем запускаем второй

                yield return StartCoroutine(LerpLight(
                    _light.pointLightInnerRadius,
                    _light.pointLightOuterRadius,
                    _innerRadius, 
                    _outerRadius 
                ));
            }
        }

        private IEnumerator LerpLight(float fromInner, float fromOuter, float toInner, float toOuter)
        {
            float timer = 0f;

            while (timer < duration)
            {
                float t = timer / duration;

                _light.pointLightInnerRadius = Mathf.Lerp(fromInner, toInner, t);
                _light.pointLightOuterRadius = Mathf.Lerp(fromOuter, toOuter, t);

                timer += Time.deltaTime;
                yield return null;
            }

            // Установим точные конечные значения
            _light.pointLightInnerRadius = toInner;
            _light.pointLightOuterRadius = toOuter;
        }
    }
}