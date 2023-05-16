using System;
using Infastructure.Character;
using Infastructure.Data;
using UnityEngine;

namespace Infastructure.StaticData.Character
{
    [Serializable]
    public class CharacterSpawnerData
    {
        public SexId SexId;
        public Vector2 Position;


        public CharacterSpawnerData(SexId sexId, Vector2 position)
        {
            SexId = sexId;
            Position = position;
        }
    }
}