using Infastructure.StaticData.Window;

namespace Infastructure.Services.Window
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}