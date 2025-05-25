using TMPro;
using UnityEngine;
using View;

namespace Controllers
{
    public class EnterPoint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        private CoinsController _coinsController;
        private ReloadLevelController _reloadLevelController;
        private ConsoleView _consoleView;
        public CoinsController CoinsController => _coinsController;
        public ReloadLevelController ReloadLevelController => _reloadLevelController;
        private void Awake()
        {
            _coinsController = new CoinsController(coinsText);
            _reloadLevelController = new ReloadLevelController();
        }

        public void ChangeCoinsText(int score)
        {
            Debug.Log(score);
            _coinsController.AddCoins(score);
        }
    }
}

