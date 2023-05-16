using Infastructure.Services;
using Infastructure.Services.SaveLoad;
using UnityEngine;

namespace Infastructure.WindowUI
{
    public class SkinsWindow : WindowBase
    {
        private ISaveLoadService _saveLoadService;
        protected override void Initialize() => 
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

        protected override void Cleanup()
        {
            base.Cleanup();
            
            _saveLoadService.SaveProgress();
            PlayerPrefs.Save();
        }
    }
}