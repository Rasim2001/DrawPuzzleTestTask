using System;
using Infastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Infastructure.Levels
{
    public class LevelHomeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IStateMachine _stateMachine;

        public void Construct(IStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void Awake() => 
            _button.onClick.AddListener(Home);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(Home);

        private void Home() => 
            _stateMachine.Enter<MainMenuState>();
    }
}