using System.Collections.Generic;
using Infastructure.Character;

namespace Infastructure.Services.CharactersObserver
{
    public interface ICharactersObserver : IService
    {
        void RegisterActionCharacters();
        List<CharacterTrigger> Characters { get; }
        void Cleanup();
    }
}