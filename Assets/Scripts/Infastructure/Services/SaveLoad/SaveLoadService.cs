using Infastructure.AssetProvider;
using Infastructure.Data;
using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public SaveLoadService(
            IUIFactory uiFactory,
            IPersistentProgressService persistentProgressService)
        {
            _uiFactory = uiFactory;
            _persistentProgressService = persistentProgressService;
        }
       

        public void SaveProgress()
        {
            foreach (ISaveProgress writer in _uiFactory.ProgressWriters)
                writer.UpdateProgress(_persistentProgressService.PlayerProgress);

            PlayerPrefs.SetString(AssetsPath.Progress, _persistentProgressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(AssetsPath.Progress)?.ToDeserialized<PlayerProgress>();
    }
}