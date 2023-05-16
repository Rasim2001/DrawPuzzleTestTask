using System;
using UnityEngine.Serialization;

namespace Infastructure.Data
{
    [Serializable]
    public class LevelData
    {
        public string LevelName;

        public LevelData(string levelName) => 
            LevelName = levelName;
    }
}