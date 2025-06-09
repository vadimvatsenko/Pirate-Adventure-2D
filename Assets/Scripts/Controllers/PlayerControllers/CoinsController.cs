using Components;
using Model;
using TMPro;
using UnityEngine;


namespace Controllers.PlayerControllers
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private CoinsComponent coinsComponent;
        private GameSession _gameSession;

        private void Awake()
        {
            coinsComponent.OnCoinsAdd += AddCoin;
            coinsComponent.OnCoinsRemove += RemoveCoin;
        }
        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();
            if (_gameSession != null)
            {
                UpdateUI();
            }
        }

        private void OnDisable()
        {
            coinsComponent.OnCoinsAdd -= AddCoin;
            coinsComponent.OnCoinsRemove -= RemoveCoin;
        }

        public void AddCoin(int amount)
        {
            _gameSession.PlayerData.coins += amount;
            UpdateUI();
        }

        public void RemoveCoin()
        {
            if (_gameSession.PlayerData.coins == 0) return;
            _gameSession.PlayerData.coins = 0;
            UpdateUI();
        }

        private void UpdateUI()
        {
            coinsText.text = _gameSession.PlayerData.coins.ToString();
        }
    }
}
