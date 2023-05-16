using System;
using System.Collections.Generic;

namespace Infastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public List<LevelData> Levels = new List<LevelData>();

        public SkinData MaleSkin;
        public SkinData FemaleSkin;

        public PlayerProgress(string levelName) => 
            Levels.Add(new LevelData(levelName));
    }
}