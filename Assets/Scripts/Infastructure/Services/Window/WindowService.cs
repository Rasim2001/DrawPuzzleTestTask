using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.PersistentProgress;
using Infastructure.StaticData.Window;
using UnityEngine;

namespace Infastructure.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public WindowService(IUIFactory uiFactory, IPersistentProgressService persistentProgressService)
        {
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Menu:
                    _uiFactory.CreateMenuWindow(windowId, this);
                    break;
                case WindowId.Levels:
                    _uiFactory.Cleanup();
                    _uiFactory.CreateLevelsWindow(windowId, this);
                    InitProgressReaders();
                    break;
                case WindowId.LevelWin:
                    _uiFactory.CreateLevelWinWindow(windowId, this);
                    break;
                case WindowId.LevelLoss:
                    _uiFactory.CreateLevelLossWindow(windowId, this);
                    break;
                case WindowId.Skins:
                    _uiFactory.Cleanup();
                    _uiFactory.CreateSkinsWindow(windowId, this);
                    InitProgressReaders();
                    break;
            }
        }

        private void InitProgressReaders()
        {
            foreach (ISaveProgressReader progressReader in _uiFactory.ProgressReaders)
                progressReader.LoadProgress(_persistentProgressService.PlayerProgress);
        }
    }
}