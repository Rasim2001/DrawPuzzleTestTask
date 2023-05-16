using System.Collections.Generic;
using Infastructure.Services.PersistentProgress;
using Infastructure.Services.Window;
using Infastructure.StaticData.Window;
using UnityEngine;

namespace Infastructure.Services.GameFactory.UI
{
    public interface IUIFactory : IService
    {
        GameObject CreateMenuRoot(string path);
        void CreateMenuWindow(WindowId windowId, IWindowService windowService);
        void CreateLevelsWindow(WindowId windowId, WindowService windowService);
        List<ISaveProgressReader> ProgressReaders { get; }
        List<ISaveProgress> ProgressWriters { get; }
        void Cleanup();
        void CreateLevelItem(Transform parent, string levelNumber);
        GameObject CreateLevelRoot(string path);
        void CreateLevelWinWindow(WindowId windowId, WindowService windowService);
        void CleanupUI();
        void CreateLevelLossWindow(WindowId windowId, WindowService windowService);
        void CreateSkinsWindow(WindowId windowId, IWindowService windowService);
    }
}