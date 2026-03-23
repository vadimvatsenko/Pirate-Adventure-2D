using UnityEngine;

namespace UI.MainMenu
{
    // это отдельный компонент от UI, на нем висит скрипт
    // MenuInstaller и MainMenuView
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private Cnob cnob;
        private MainMenuView _view;
        
        private MainMenuPresenter _mainMenuPresenter;
        private MainMenuModel _mainMenuModel;

        // будет первым
        private void Awake()
        {
            _view = GetComponent<MainMenuView>();
            _mainMenuModel = new MainMenuModel();
            
        }
        private void Start()
        {
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuModel, _view, this, cnob);
            _mainMenuPresenter.Init();
        }
        
        private void OnDestroy() => _mainMenuPresenter.Dispose();
    }
}