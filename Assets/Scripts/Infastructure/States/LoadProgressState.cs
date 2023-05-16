using Infastructure.Character;
using Infastructure.Data;
using Infastructure.Services.PersistentProgress;
using Infastructure.Services.SaveLoad;
using UnityEngine;

namespace Infastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(StateMachine stateMachine, SceneLoader sceneLoader,
            IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _persistentProgressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();


        private PlayerProgress NewProgress()
        {
            Debug.Log("New progress");
            _persistentProgressService.PlayerProgress = new PlayerProgress("Level_1");
            _persistentProgressService.PlayerProgress.Levels.Add(new LevelData("Level_2"));
            _persistentProgressService.PlayerProgress.Levels.Add(new LevelData("Level_3"));
            _persistentProgressService.PlayerProgress.Levels.Add(new LevelData("Level_4"));
            _persistentProgressService.PlayerProgress.Levels.Add(new LevelData("Level_5"));
            _persistentProgressService.PlayerProgress.Levels.Add(new LevelData("Level_6"));

            _persistentProgressService.PlayerProgress.MaleSkin = new SkinData(CharacterTypeId.Pet1);
            _persistentProgressService.PlayerProgress.FemaleSkin = new SkinData(CharacterTypeId.Pet2);

            return _persistentProgressService.PlayerProgress;
        }
    }
}