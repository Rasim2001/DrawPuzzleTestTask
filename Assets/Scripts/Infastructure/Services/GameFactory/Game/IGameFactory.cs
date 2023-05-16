using Infastructure.Character;
using Infastructure.Data;
using UnityEngine;

namespace Infastructure.Services.GameFactory.Game
{
    public interface IGameFactory : IService
    {
        GameObject CreateCharacter(SexId sexId, CharacterTypeId characterType, Vector2 position);
        GameObject CreateTouchDetector(string path);
        GameObject CreateHud(string path);
        int MaxCharacters { get; set; }
        float LevelTime { get; set; }
        void Cleanup();
    }
}