using System.Collections.Generic;
using Infastructure.StaticData.Character;
using UnityEngine;

namespace Infastructure.StaticData.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public string NextLevelKey;
        
        public float LevelTime;

        public List<CharacterSpawnerData> CharacterSpawners;
    }
}