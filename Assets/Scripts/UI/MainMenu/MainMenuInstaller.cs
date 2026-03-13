using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenuInstaller : MonoBehaviour
    {
        [SerializeField] private MainMenuView view;
        
        private MainMenuPresenter _presenter;
        private MainMenuModel _model;

        private void Awake()
        {
            _model = new MainMenuModel();
            _presenter = new MainMenuPresenter(_model, view);
            _presenter.Init();
        }

        private void OnDestroy() => _presenter.Dispose();
    }
}