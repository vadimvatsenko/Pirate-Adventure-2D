using UnityEngine;

namespace UI.MainMenu
{
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private MainMenuView view;
        
        private MainMenuPresenter _mainMenuPresenter;
        private MainMenuModel _mainMenuModel;

        private void Awake()
        {
            _mainMenuModel = new MainMenuModel();
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuModel, view, this);
            _mainMenuPresenter.Init();
        }

        private void OnDestroy() => _mainMenuPresenter.Dispose();
    }
}