using Infastructure.Data;

namespace Infastructure.Services.PersistentProgress
{
    public interface ISaveProgress : ISaveProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }

    public interface ISaveProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}