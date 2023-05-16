using System.Collections.Generic;
using Infastructure.AssetProvider;
using Infastructure.Character;
using Infastructure.Data;
using Infastructure.Services.CharactersObserver;
using Infastructure.Services.GameFactory.Game;
using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.LevelObserver;
using Infastructure.Services.PersistentProgress;
using Infastructure.StaticData;
using Infastructure.StaticData.Character;
using Infastructure.StaticData.Level;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Infastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IStateMachine _stateMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IUIFactory _uiFactory;
        private readonly ICharactersObserver _charactersObserver;
        private readonly ILevelObserver _levelObserver;


        public LoadLevelState(
            IStateMachine stateMachine,
            SceneLoader sceneLoader,
            IStaticDataService staticData,
            IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService,
            IUIFactory uiFactory,
            ICharactersObserver charactersObserver,
            ILevelObserver levelObserver
        )
        {
            _sceneLoader = sceneLoader;
            _staticData = staticData;
            _gameFactory = gameFactory;
            _stateMachine = stateMachine;
            _persistentProgressService = persistentProgressService;
            _uiFactory = uiFactory;
            _charactersObserver = charactersObserver;
            _levelObserver = levelObserver;
        }


        public void Enter(string sceneName)
        {
            Cleanup();

            _sceneLoader.Load(sceneName, OnLoaded);
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }

        private void OnLoaded() =>
            InitGameWorld();

        private void Cleanup()
        {
            _gameFactory.Cleanup();
            _uiFactory.CleanupUI();
            _charactersObserver.Cleanup();
        }

        private void InitGameWorld()
        {
            InitCharacters();
            InitTouchDetector();
            InitHud();
            InitLevelRootUI();
        }

        private void InitCharacters()
        {
            string sceneKey = SceneManager.GetActiveScene().name;
            LevelStaticData levelData = _staticData.ForLevel(sceneKey);

            _levelObserver.CurrentLevel = levelData.LevelKey;
            _levelObserver.NextLevel = levelData.NextLevelKey;

            _gameFactory.MaxCharacters = levelData.CharacterSpawners.Count;
            _gameFactory.LevelTime = levelData.LevelTime;

            SkinData maleSkin = _persistentProgressService.PlayerProgress.MaleSkin;
            SkinData femaleSkin = _persistentProgressService.PlayerProgress.FemaleSkin;
           

            foreach (CharacterSpawnerData spawnerData in levelData.CharacterSpawners)
            {
                GameObject character;

                if (spawnerData.SexId == SexId.Male)
                    character = _gameFactory.CreateCharacter(spawnerData.SexId, maleSkin.CharacterTypeId,
                        spawnerData.Position);
                else
                    character = _gameFactory.CreateCharacter(spawnerData.SexId, femaleSkin.CharacterTypeId,
                        spawnerData.Position);

                _charactersObserver.Characters.Add(character.GetComponent<CharacterTrigger>());
            }

            _charactersObserver.RegisterActionCharacters();
        }

        private void InitTouchDetector() =>
            _gameFactory.CreateTouchDetector(AssetsPath.TouchDetectorPath);

        private void InitHud() =>
            _gameFactory.CreateHud(AssetsPath.HUDPath);

        private void InitLevelRootUI() =>
            _uiFactory.CreateLevelRoot(AssetsPath.LevelRootPath);
    }
}