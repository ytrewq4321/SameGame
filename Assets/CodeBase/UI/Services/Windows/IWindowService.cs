using CodeBase.Infrastructure.Services;

namespace CodeBase.UI.Services.Factory.Windows
{
    public interface IWindowService : IService
    {
        public void Open(WindowId windowId);
    }
}
