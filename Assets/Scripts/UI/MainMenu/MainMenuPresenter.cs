using System;
using System.Collections;
using GameManagerInfo;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuPresenter: IDisposable
    {
        private static readonly int Hide = Animator.StringToHash("hide");
        private static readonly int Show = Animator.StringToHash("show");
        private static readonly int NextLevel = Animator.StringToHash("nextLevel");

        private readonly MonoBehaviour _coroutineRunner;
        private readonly MainMenuModel _model;
        private readonly MainMenuView _view;

        private Cnob _cnob;
        private AudioSource _mainTheme;

        public MainMenuPresenter(MainMenuModel model, MainMenuView view, MonoBehaviour coroutineRunner, Cnob cnob, AudioSource mainTheme)
        {
            _model = model;
            _view = view;
            _cnob = cnob;
            _mainTheme = mainTheme;
            
            _coroutineRunner = coroutineRunner;

            _view.OnPlayClicked += PlayGame;
            _view.OnQuitClicked += QuitGame;
            _view.OnMainMenuClicked += OpenMainMenu;
            _view.OnOptionsClicked += OpenOptionsMenu;

            _cnob.onFinish += StartLevel;
        }
        
        public void Dispose()
        {
            _view.OnPlayClicked -= PlayGame;
            _view.OnQuitClicked -= QuitGame;
            _view.OnMainMenuClicked -= OpenMainMenu;
            _view.OnOptionsClicked -= OpenOptionsMenu;
            
            _cnob.onFinish -= StartLevel;
        }

        public void Init() => OpenMainMenu();
        
        private void OpenMainMenu() => _coroutineRunner.StartCoroutine(OpenMainMenuRoutine());
        
        private IEnumerator OpenMainMenuRoutine()
        {
            if (_view.OptionsWindowAnimator.isActiveAndEnabled)
            {
                _view.OptionsWindowAnimator.SetTrigger(Hide);
                
                // ждём, пока Animator войдёт в нужный state
                while (!_view.OptionsWindowAnimator.GetCurrentAnimatorStateInfo(0).IsName("hide"))
                    yield return null;

                // ждём, пока анимация не доиграет
                while (_view.OptionsWindowAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                    yield return null;
            }
            
            _model.SetWindow(MenuWindowType.Main);

            if (_view != null)
            {
                _view.ShowWindow(_model.CurrentWindowType);
                _view.MainMenuAnimator.SetTrigger(Show);
            }
        }
        
        private void OpenOptionsMenu() => _coroutineRunner.StartCoroutine(OpenOptionsMenuRoutine());
        
        private IEnumerator OpenOptionsMenuRoutine()
        {
            _view.MainMenuAnimator.SetTrigger(Hide);

            // ждём, пока Animator войдёт в нужный state
            while (!_view.MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("hide"))
                yield return null;

            // ждём, пока анимация не доиграет
            while (_view.MainMenuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                yield return null;
            
            _model.SetWindow(MenuWindowType.Options);
            _view.ShowWindow(_model.CurrentWindowType);
            _view.OptionsWindowAnimator.SetTrigger(Show);
        }
        
        
        private void PlayGame() => _coroutineRunner.StartCoroutine(StartGameRoutine());

        private IEnumerator StartGameRoutine()
        {
            _view.PlayButton.interactable = false;
            _view.OptionsButton.interactable = false;
            _view.ExitButton.interactable = false;
            _view.BackToMainMenuButton.interactable = false;
            
            //_view.MainMenuAnimator.SetTrigger(NextLevel);
            
            _cnob.AnimateRound();
            
            /*// нужно дождаться окончания анимации
            while (!_view.MainMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("nextLevel"))
                yield return null;
            
            // ждём, пока анимация не доиграет
            while (_view.MainMenuAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                yield return null;

            
            SceneManager.LoadScene("Level 1");*/

            yield return null;
        }
        
        private void StartLevel() => SceneManager.LoadScene("Level 1");
        
        private void QuitGame()
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
    }
}