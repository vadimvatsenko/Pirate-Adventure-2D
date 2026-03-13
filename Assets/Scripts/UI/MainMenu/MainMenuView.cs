using System;
using UI.Widgets;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private CustomButton playButton;
        [SerializeField] private CustomButton optionButton;
        [SerializeField] private CustomButton exitButton;
        [SerializeField] private CustomButton mainMenuButton;
        
        [Header("Windows")]
        [SerializeField] private GameObject mainWindow;
        [SerializeField] private GameObject optionsWindow;

        public event Action OnOptionsClicked;
        public event Action OnPlayClicked;
        public event Action OnQuitClicked;
        public event Action OnMainMenuClicked;
        
        private void HandleOptionsClicked() => OnOptionsClicked?.Invoke();
        private void HandlePlayClicked() => OnPlayClicked?.Invoke();
        private void HandleQuitClicked() => OnQuitClicked?.Invoke();
        private void HandleMainMenuClicked() => OnMainMenuClicked?.Invoke();

        private void OnEnable()
        {
            playButton.onClick.AddListener(HandlePlayClicked);
            optionButton.onClick.AddListener(HandleOptionsClicked);
            exitButton.onClick.AddListener(HandleQuitClicked);
            mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(HandlePlayClicked);
            optionButton.onClick.RemoveListener(HandleOptionsClicked);
            exitButton.onClick.RemoveListener(HandleQuitClicked);
            mainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
        }

        public void ShowWindow(MenuWindowType windowType)
        {
            mainWindow.SetActive(windowType == MenuWindowType.Main);
            optionsWindow.SetActive(windowType == MenuWindowType.Options);
        }
    }
}