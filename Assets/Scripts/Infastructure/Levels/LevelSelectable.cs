using System;
using Infastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Infastructure.Levels
{
    public class LevelSelectable : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public TextMeshProUGUI LevelName;


        private IStateMachine _stateMachine;

        private void Awake() =>
            _button.onClick.AddListener(OpenLevelState);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OpenLevelState);

        public void Contruct(IStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        private void OpenLevelState() =>
            _stateMachine.Enter<LoadLevelState, string>(LevelName.text);
    }
}