using System;
using System.Collections.Generic;
using System.Linq;
using Infastructure.AssetProvider;
using Infastructure.Character;
using Infastructure.Data;
using Infastructure.StaticData.Character;
using Infastructure.StaticData.Level;
using Infastructure.StaticData.Window;
using UnityEngine;

namespace Infastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, WindowConfig> _windows;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<CharacterTypeId, CharacterStaticData> _characters;

        public void LoadStaticData()
        {
            _windows = Resources.Load<WindowStaticData>(AssetsPath.WindowStaticDataPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(AssetsPath.LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);

            _characters = Resources.LoadAll<CharacterStaticData>(AssetsPath.CharacterStaticDataPath)
                .ToDictionary(x => x.CharacterTypeId, x => x);
        }


        public WindowConfig ForWindow(WindowId windowId)
        {
            if (_windows.TryGetValue(windowId, out WindowConfig config))
                return config;

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData leveData))
                return leveData;

            return null;
        }
        
        public CharacterStaticData ForCharacter(CharacterTypeId characterTypeId)
        {
            if (_characters.TryGetValue(characterTypeId, out CharacterStaticData characterData))
                return characterData;

            return null;
        }
    }
}