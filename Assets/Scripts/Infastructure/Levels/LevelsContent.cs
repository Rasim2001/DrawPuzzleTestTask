using Infastructure.Data;
using Infastructure.Services.GameFactory.UI;
using Infastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infastructure.Levels
{
    public class LevelsContent : MonoBehaviour, ISaveProgressReader
    {
        private IUIFactory _iuiFactory;

        public void Construct(IUIFactory iuiFactory) => 
            _iuiFactory = iuiFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.Levels.Count > 0) 
                FillingLevels(progress);
        }

        private void FillingLevels(PlayerProgress progress)
        {
            for (int i = 0; i < progress.Levels.Count; i++)
                _iuiFactory.CreateLevelItem(gameObject.transform, progress.Levels[i].LevelName);
        }

    }
}