using System;
using System.Collections.Generic;
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
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;

        private IExitableState _activeState;

        public StateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),

                [typeof(LoadProgressState)] =
                    new LoadProgressState(
                        this,
                        sceneLoader,
                        services.Single<IPersistentProgressService>(),
                        services.Single<ISaveLoadService>()),

                [typeof(MainMenuState)] =
                    new MainMenuState(
                        this,
                        sceneLoader,
                        services.Single<IUIFactory>(),
                        services.Single<IWindowService>()),

                [typeof(LoadLevelState)] =
                    new LoadLevelState(
                        this,
                        sceneLoader,
                        services.Single<IStaticDataService>(),
                        services.Single<IGameFactory>(),
                        services.Single<IPersistentProgressService>(),
                        services.Single<IUIFactory>(), 
                        services.Single<ICharactersObserver>(), 
                        services.Single<ILevelObserver>()),

                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var newState = ChangeState<TState>();
            newState.Enter();
        }

        public void Enter<TState, TPaylodad>(TPaylodad paylodad) where TState : class, IPayloadState<TPaylodad>
        {
            var newState = ChangeState<TState>();
            newState.Enter(paylodad);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState newState = GetState<TState>();
            _activeState = newState;
            return newState;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}