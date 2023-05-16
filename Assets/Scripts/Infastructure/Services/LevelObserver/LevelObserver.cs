namespace Infastructure.Services.LevelObserver
{
    public class LevelObserver : ILevelObserver
    {
        public string CurrentLevel { get; set; }
        public string NextLevel { get; set; }
    }
}