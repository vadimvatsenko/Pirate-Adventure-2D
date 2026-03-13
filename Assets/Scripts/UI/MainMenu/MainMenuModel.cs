namespace UI.MainMenu
{
    public class MainMenuModel
    {
        public MenuWindowType CurrentWindowType { get; private set; }
        
        public void SetWindow(MenuWindowType windowType) 
            => CurrentWindowType = windowType;
    }
}