using Infastructure.AssetProvider;
using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.Window;
using Infastructure.StaticData.Window;
using UnityEngine;

namespace Infastructure.States
{
    public class MainMenuState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windowService;

        private GameObject MenuRoot;


        public MainMenuState(
            StateMachine stateMachine,
            SceneLoader sceneLoader, 
            IUIFactory uiFactory,
            IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowService = windowService;
        }

        public void Enter() =>
            _sceneLoader.Load(AssetsPath.Menu, OnLoaded);

        public void Exit() =>
            Object.Destroy(MenuRoot);

        private void OnLoaded()
        {
            MenuRoot = _uiFactory.CreateMenuRoot(AssetsPath.MenuRootPath);

            _windowService.Open(WindowId.Menu);
        }
    }
}