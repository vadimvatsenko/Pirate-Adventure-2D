using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using View;

namespace Controllers
{
    public class CoinsController : MonoBehaviour
    {
        [FormerlySerializedAs("_coinsText")] [SerializeField] private TextMeshProUGUI coinsText;
        private int _coins;
        
        public void AddCoins(int cost)
        {
            _coins += cost;
            coinsText.text = "Coins: " + _coins;
        }

        public void RemoveCoins(int cost)
        {
            _coins -= cost;
            coinsText.text = "Coins: " + _coins;
        }
    }
}

