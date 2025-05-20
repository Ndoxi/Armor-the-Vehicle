namespace Codebase.Core
{
    public readonly struct LevelData
    {
        public readonly int DistanseToComplete;

        public LevelData(int distanseToComplete)
        {
            DistanseToComplete = distanseToComplete;
        }
    }
}