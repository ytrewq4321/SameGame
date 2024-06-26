namespace CodeBase.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        LevelStaticData ForLevel(int level);
    }
}