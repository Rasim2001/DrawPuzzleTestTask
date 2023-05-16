using Infastructure.Character;
using Infastructure.Data;
using UnityEngine;

namespace Infastructure.StaticData.Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "StaticData/Character")]
    public class CharacterStaticData : ScriptableObject
    {
        public CharacterTypeId CharacterTypeId;
        public GameObject Prefab;
    }
}