using Infastructure.AssetProvider;
using Infastructure.Character;
using Infastructure.Data;
using Infastructure.Levels;
using Infastructure.Services.Window;
using Infastructure.States;
using Infastructure.StaticData;
using Infastructure.StaticData.Character;
using Infastructure.Touch;
using UnityEngine;

namespace Infastructure.Services.GameFactory.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assets;
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;

        public int MaxCharacters { get; set; }
        public float LevelTime { get; set; }

        private GameObject _hud;
        private GameObject _touchDetector;

        public GameFactory(
            IStaticDataService staticData,
            IAssetProvider assets,
            IStateMachine stateMachine,
            IWindowService windowService)
        {
            _staticData = staticData;
            _assets = assets;
            _stateMachine = stateMachine;
            _windowService = windowService;
        }

        public GameObject CreateCharacter(SexId sexId, CharacterTypeId characterType, Vector2 position)
        {
            CharacterStaticData characterData = _staticData.ForCharacter(characterType);

            GameObject characterObj = Object.Instantiate(characterData.Prefab, position, Quaternion.identity);
            var character = characterObj.GetComponent<CharacterTrigger>();
            character.SexId = sexId;

            return characterObj;
        }

        public GameObject CreateTouchDetector(string path)
        {
            _touchDetector = _assets.Instantiate(path);
            var touch = _touchDetector.GetComponent<TouchDetector>();
            touch.Construct(new FinishTimeCalculator(LevelTime), MaxCharacters);

            return _touchDetector;
        }

        public GameObject CreateHud(string path)
        {
            _hud = _assets.Instantiate(path);

            var levelHomeButton = _hud.GetComponentInChildren<LevelHomeButton>();
            levelHomeButton.Construct(_stateMachine);

            return _hud;
        }


        public void Cleanup()
        {
            Object.Destroy(_hud);
            Object.Destroy(_touchDetector);

            MaxCharacters = 0;
        }
    }
}