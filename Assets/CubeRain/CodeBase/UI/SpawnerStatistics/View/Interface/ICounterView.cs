namespace Assets.CubeRain.CodeBase.UI.SpawnerStatistics.View.Interface
{
    public interface ICounterView
    {
        public void UpdateOverallCount(string count);
        public void UpdateCreatedCount(string count);
        public void UpdateActiveCount(string count);
    }
}
