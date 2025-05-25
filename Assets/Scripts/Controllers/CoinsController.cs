using TMPro;
using View;

namespace Controllers
{
    public class CoinsController
    {
        private TextMeshProUGUI _coinsText;
        private int _startCoins;
        public CoinsController(TextMeshProUGUI coinsText)
        {
            _startCoins = 0;
            _coinsText = coinsText;
            _coinsText.text = "Coins: " + _startCoins;
        }
    
        public void AddCoins(int cost)
        {
            _startCoins += cost;
            _coinsText.text = "Coins: " + _startCoins;
        }
    }
}

