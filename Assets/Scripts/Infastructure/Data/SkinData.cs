using System;

namespace Infastructure.Data
{
    [Serializable]
    public class SkinData
    {
        public CharacterTypeId CharacterTypeId;

        public SkinData(CharacterTypeId characterTypeId) =>
            CharacterTypeId = characterTypeId;
    }
}