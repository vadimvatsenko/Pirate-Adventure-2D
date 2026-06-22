using GameManagerInfo;
using UnityEngine;
using Utils;

namespace UI.MainMenu
{
    // это отдельный компонент от UI, на нем висит скрипт
    // MenuInstaller и MainMenuView
    public class MenuInstaller : MonoBehaviour
    {
        [SerializeField] private Cnob cnob;
        [SerializeField] private AudioSource _mainTheme;
        private float _percent;
        
        
        public AudioSource MainTheme => _mainTheme;
        private MainMenuView _view;
        private MainMenuPresenter _mainMenuPresenter;
        private MainMenuModel _mainMenuModel;

        // будет первым
        private void Awake()
        {
            _mainTheme.volume = PlayerPrefs.GetFloat(GradientsInfo.Value, _percent);
            _view = GetComponent<MainMenuView>();
            _mainMenuModel = new MainMenuModel();
            
        }
        private void Start()
        {
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuModel, _view, this, cnob, _mainTheme);
            _mainMenuPresenter.Init();
        }
        
        private void OnDestroy() => _mainMenuPresenter.Dispose();
    }
}