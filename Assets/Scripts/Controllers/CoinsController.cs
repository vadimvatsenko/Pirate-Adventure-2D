using Components;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using View;

namespace Controllers
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private CoinsComponent coinsComponent;

        private void Start()
        {
            coinsComponent.OnScoreChanged += AddCoins;
        }
        
        public void AddCoins(int cost)
        {
            coinsText.text = coinsComponent.Score.ToString();
        }

        public void RemoveCoins(int cost)
        {
            coinsText.text = coinsComponent.Score.ToString();
        }
    }
}

