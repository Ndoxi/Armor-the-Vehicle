using System;

namespace Codebase.Core
{
    public class LevelProgress : IProgress<int>
    {
        public event Action<int> OnValueUpdate;

        public int TargetValue => _targetValue;
        public int CurrentValue => _currentValue;

        private readonly int _targetValue;
        private int _currentValue;

        public LevelProgress(LevelData levelData)
        {
            _targetValue = levelData.DistanseToComplete;
            _currentValue = 0;
        }

        public void Report(int value)
        {
            _currentValue = value;
            OnValueUpdate?.Invoke(value);
        }

        public void Reset()
        {
            Report(0);
        }
    }
}