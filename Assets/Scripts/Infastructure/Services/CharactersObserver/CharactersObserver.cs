using System;
using System.Collections.Generic;
using System.Linq;
using Infastructure.Character;
using Infastructure.Services.Window;
using Infastructure.StaticData.Window;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infastructure.Services.CharactersObserver
{
    public class CharactersObserver : ICharactersObserver
    {
        public List<CharacterTrigger> Characters { get; } = new List<CharacterTrigger>();

        private readonly IWindowService _windowService;

        private bool _isWinOpened = false;
        private bool _isLossOpened = false;

        public CharactersObserver(IWindowService windowService) =>
            _windowService = windowService;

        public void RegisterActionCharacters()
        {
            foreach (CharacterTrigger character in Characters)
            {
                character.OnWinHappened += OpenLevelWinWindow;
                character.OnLossHappened += OpenLevelLossWindow;
            }
        }


        private void OpenLevelWinWindow()
        {
            if (!_isWinOpened && !_isLossOpened)
                _windowService.Open(WindowId.LevelWin);

            _isWinOpened = true;
        }

        private void OpenLevelLossWindow()
        {
            if (!_isLossOpened)
                _windowService.Open(WindowId.LevelLoss);

            _isLossOpened = true;
        }

        public void Cleanup()
        {
            for (int i = Characters.Count - 1; i >= 0; i--)
            {
                Object.Destroy(Characters[i]); //.gameObject does not work
                Characters.RemoveAt(i);
            }

            _isWinOpened = false;
            _isLossOpened = false;
        }
    }
}