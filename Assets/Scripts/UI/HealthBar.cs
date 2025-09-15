using System.Collections;
using Creatures.CreaturesHealth;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Image _healthBarImage;
        [SerializeField] private CreatureHealth health;

        private void Awake() => _healthBarImage = GetComponent<Image>();
        
        private void OnEnable()
        {
            health.OnHealthChange += ChangeHelthBarValue;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= ChangeHelthBarValue;
        }

        private void ChangeHelthBarValue(float prevHealth, float health ) => StartCoroutine(SmoothChangeHelthBarValue(prevHealth, health));

        private IEnumerator SmoothChangeHelthBarValue(float prevHealth, float health)
        {
            float duration = 0.5f;
            float elapsedTime = 0;

            // test
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                float percentageComplete = Mathf.Clamp01(elapsedTime / duration);

                float currentHealth = Mathf.Lerp(prevHealth, health, percentageComplete);

                _healthBarImage.fillAmount = currentHealth / 100f;

                _healthBarImage.color = Color.Lerp(Color.red, Color.green, _healthBarImage.fillAmount);
                
                yield return null;
            }
        }
    }
}