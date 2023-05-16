namespace Infastructure.Services.LevelObserver
{
    public interface ILevelObserver : IService
    {
        string CurrentLevel { get; set; }
        string NextLevel { get; set; }
    }
}