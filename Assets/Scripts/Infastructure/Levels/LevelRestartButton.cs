using System;
using Infastructure.Services.LevelObserver;
using Infastructure.Services.Window;
using Infastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Infastructure.Levels
{
    public class LevelRestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IStateMachine _stateMachine;
        private ILevelObserver _levelObserver;

        public void Conctruct(IStateMachine stateMachine, ILevelObserver levelObserver)
        {
            _stateMachine = stateMachine;
            _levelObserver = levelObserver;
        }

        private void Awake() => 
            _button.onClick.AddListener(Restart);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(Restart);

        private void Restart() => 
            _stateMachine.Enter<LoadLevelState, string>(_levelObserver.CurrentLevel);
    }
}