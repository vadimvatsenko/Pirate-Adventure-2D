namespace UI.MainMenu
{
    // тип текущего окна и переключение окон
    public class MainMenuModel
    {
        public MenuWindowType CurrentWindowType { get; private set; }
        public void SetWindow(MenuWindowType windowType) => CurrentWindowType = windowType;
    }
}