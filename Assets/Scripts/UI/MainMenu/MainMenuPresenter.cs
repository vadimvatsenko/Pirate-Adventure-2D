using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuPresenter: IDisposable
    {
        private readonly MainMenuModel _model;
        private readonly MainMenuView _view;

        public MainMenuPresenter(MainMenuModel model, MainMenuView view)
        {
            _model = model;
            _view = view;

            _view.OnPlayClicked += PlayGame;
            _view.OnQuitClicked += QuitGame;
            _view.OnMainMenuClicked += OpenMainMenu;
            _view.OnOptionsClicked += OpenOptionsMenu;
        }
        
        public void Dispose()
        {
            _view.OnPlayClicked -= PlayGame;
            _view.OnQuitClicked -= QuitGame;
            _view.OnMainMenuClicked -= OpenMainMenu;
            _view.OnOptionsClicked -= OpenOptionsMenu;
        }

        public void Init()
        {
            OpenMainMenu();
        }
        
        private void OpenMainMenu()
        {
            _model.SetWindow(MenuWindowType.Main);
            _view.ShowWindow(_model.CurrentWindowType);
        }

        private void OpenOptionsMenu()
        {
            _model.SetWindow(MenuWindowType.Options);
            _view.ShowWindow(_model.CurrentWindowType);
        }

        private void PlayGame()
        {
            Debug.Log("Playing Game");
            SceneManager.LoadScene("Level 1");
        }

        private void QuitGame()
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }
}