using TMPro;
using UnityEngine;
using View;

namespace Controllers
{
    public class EnterPoint : MonoBehaviour
    {
        private ReloadLevelController _reloadLevelController;
        private CoinsController _coinsController;
        private ConsoleView _consoleView;
        private TextMeshProUGUI _coinsText;
        public ReloadLevelController ReloadLevelController => _reloadLevelController;
        public CoinsController CoinsController => _coinsController;
        public ConsoleView ConsoleView => _consoleView;
        public TextMeshProUGUI CoinsText => _coinsText;
        
        
        
        private void Awake()
        {
            // контроллеры
            _reloadLevelController = new ReloadLevelController();
            _consoleView = new ConsoleView();
            _coinsController = new CoinsController(_reloadLevelController, _consoleView);
        
            //вид
        }
    }
}

