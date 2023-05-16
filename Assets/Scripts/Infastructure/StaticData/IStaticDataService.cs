using Infastructure.Character;
using Infastructure.Data;
using Infastructure.States;
using Infastructure.StaticData.Character;
using Infastructure.StaticData.Level;
using Infastructure.StaticData.Window;

namespace Infastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        WindowConfig ForWindow(WindowId windowId);
        LevelStaticData ForLevel(string sceneKey);
        CharacterStaticData ForCharacter(CharacterTypeId characterTypeId);
    }
}