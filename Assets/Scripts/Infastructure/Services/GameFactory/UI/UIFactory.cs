using System.Collections.Generic;
using Infastructure.AssetProvider;
using Infastructure.Levels;
using Infastructure.Services.LevelObserver;
using Infastructure.Services.PersistentProgress;
using Infastructure.Services.Window;
using Infastructure.States;
using Infastructure.StaticData;
using Infastructure.StaticData.Window;
using Infastructure.WindowUI;
using UnityEngine;

namespace Infastructure.Services.GameFactory.UI
{
    public class UIFactory : IUIFactory
    {
        public List<ISaveProgressReader> ProgressReaders { get; } = new List<ISaveProgressReader>();

        public List<ISaveProgress> ProgressWriters { get; } = new List<ISaveProgress>();

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IStateMachine _stateMachine;
        private readonly ILevelObserver _levelObserver;

        private GameObject _menuRootUI;
        private GameObject _levelRootUI;

        public UIFactory(
            IAssetProvider assets,
            IStaticDataService staticData,
            IStateMachine stateMachine,
            ILevelObserver levelObserver)
        {
            _assets = assets;
            _staticData = staticData;
            _stateMachine = stateMachine;
            _levelObserver = levelObserver;
        }


        public GameObject CreateMenuRoot(string path) =>
            _menuRootUI = _assets.Instantiate(path);

        public GameObject CreateLevelRoot(string path) =>
            _levelRootUI = _assets.Instantiate(path);

        public void CreateMenuWindow(WindowId windowId, IWindowService windowService)
        {
            WindowConfig windowConfig = _staticData.ForWindow(windowId);
            InstantiateWithRegistration(windowService, windowConfig, _menuRootUI.transform);
        }

        public void CreateSkinsWindow(WindowId windowId, IWindowService windowService)
        {
            WindowConfig windowConfig = _staticData.ForWindow(windowId);
            InstantiateWithRegistration(windowService, windowConfig, _menuRootUI.transform);
        }
        
        public void CreateLevelsWindow(WindowId windowId, WindowService windowService)
        {
            WindowConfig windowConfig = _staticData.ForWindow(windowId);
            var window = InstantiateWithRegistration(windowService, windowConfig, _menuRootUI.transform);

            var levelContent = window.GetComponentInChildren<LevelsContent>();
            levelContent.Construct(this);
        }

        public void CreateLevelWinWindow(WindowId windowId, WindowService windowService)
        {
            WindowConfig windowConfig = _staticData.ForWindow(windowId);
            var window = InstantiateWithRegistration(windowService, windowConfig, _levelRootUI.transform);

            var restartButton = window.GetComponentInChildren<LevelRestartButton>();
            restartButton.Conctruct(_stateMachine, _levelObserver);

            var levelTransferButton = window.GetComponentInChildren<LevelTransferButton>();
            levelTransferButton.Conctruct(_stateMachine, _levelObserver);

            var levelHomeButton = window.GetComponentInChildren<LevelHomeButton>();
            levelHomeButton.Construct(_stateMachine);
        }

        public void CreateLevelLossWindow(WindowId windowId, WindowService windowService)
        {
            WindowConfig windowConfig = _staticData.ForWindow(windowId);
            var window = InstantiateWithRegistration(windowService, windowConfig, _levelRootUI.transform);
            
            var restartButton = window.GetComponentInChildren<LevelRestartButton>();
            restartButton.Conctruct(_stateMachine, _levelObserver);
            
            var levelHomeButton = window.GetComponentInChildren<LevelHomeButton>();
            levelHomeButton.Construct(_stateMachine);
        }


        public void CreateLevelItem(Transform parent, string levelNumber)
        {
            var levelItem = _assets.Instantiate(AssetsPath.OpenLevelItemPath, parent);

            var levelSelectable = levelItem.GetComponent<LevelSelectable>();
            levelSelectable.Contruct(_stateMachine);

            levelSelectable.LevelName.text = levelNumber;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public void CleanupUI() => 
            Object.Destroy(_levelRootUI);

        private GameObject InstantiateWithRegistration(IWindowService windowService, WindowConfig windowConfig,Transform parent)
        {
            var window = Object.Instantiate(windowConfig.Prefab, parent);

            foreach (OpenWindowButton windowButton in window.GetComponentsInChildren<OpenWindowButton>())
                windowButton.Construct(windowService);

            RegisterWatchers(window);

            return window;
        }

        private void RegisterWatchers(GameObject window)
        {
            foreach (ISaveProgressReader progressReader in window.GetComponentsInChildren<ISaveProgressReader>())
            {
                if (progressReader is ISaveProgress progressWriter)
                    ProgressWriters.Add(progressWriter);

                ProgressReaders.Add(progressReader);
            }
        }
    }
}