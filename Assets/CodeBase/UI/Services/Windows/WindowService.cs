

namespace CodeBase.UI.Services.Factory.Windows
{
    public class WindowService :  IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.LoseWindow:
                    _uiFactory.CreateLoseWindow();
                    break;
                case WindowId.WinWindow:
                    _uiFactory.CreateWinWindow();
                    break;
            }
        }
    }
}
