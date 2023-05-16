using Infastructure.AssetProvider;
using Infastructure.Services;
using Infastructure.Services.CharactersObserver;
using Infastructure.Services.GameFactory.Game;
using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.LevelObserver;
using Infastructure.Services.PersistentProgress;
using Infastructure.Services.SaveLoad;
using Infastructure.Services.Window;
using Infastructure.StaticData;
using UnityEngine;

namespace Infastructure.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _allServices;

        public BootstrapState(StateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;

            RegisterServices();
        }


        public void Enter() =>
            _sceneLoader.Load(AssetsPath.Initial, OnLoaded);

        private void OnLoaded()
        {
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStaticData();

            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider.AssetProvider());
            _allServices.RegisterSingle<IStateMachine>(_stateMachine);
            _allServices.RegisterSingle<ILevelObserver>(new LevelObserver());

            _allServices.RegisterSingle<IUIFactory>(
                new UIFactory(
                    _allServices.Single<IAssetProvider>(),
                    _allServices.Single<IStaticDataService>(),
                    _stateMachine,
                    _allServices.Single<ILevelObserver>()));

            _allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _allServices.RegisterSingle<IWindowService>(
                new WindowService(
                    _allServices.Single<IUIFactory>(),
                    _allServices.Single<IPersistentProgressService>()));

            _allServices.RegisterSingle<IGameFactory>(
                new GameFactory(
                    _allServices.Single<IStaticDataService>(),
                    _allServices.Single<IAssetProvider>(),
                    _stateMachine,
                    _allServices.Single<IWindowService>()));

            _allServices.RegisterSingle<ICharactersObserver>(
                new CharactersObserver(_allServices.Single<IWindowService>()));

            _allServices.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(
                    _allServices.Single<IUIFactory>(),
                    _allServices.Single<IPersistentProgressService>()));
        }

        private void RegisterStaticData()
        {
            var staticData = new StaticDataService();
            staticData.LoadStaticData();
            _allServices.RegisterSingle<IStaticDataService>(staticData);
        }
    }
}