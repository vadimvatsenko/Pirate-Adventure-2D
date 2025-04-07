using TMPro;
using UnityEngine;

namespace Components.UI
{
    public class HealthUI : MonoBehaviour
    {
        private TextMeshProUGUI _healthText;

        private void Awake()
        {
            _healthText = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeHeath(string health)
        {
            _healthText.text = $"Health: {health}";
        }
    }
}