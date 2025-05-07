using System;
using UnityEngine;

namespace Codebase.Core.Actors
{
    public class ActorHealth
    {
        public event Action<int> OnValueChanged;

        public int Initial => _initialValue;
        public int Current => _currentValue;

        private readonly int _initialValue;
        private int _currentValue;

        public ActorHealth(int initialValue)
        {
            if (initialValue < 0)
                throw new ArgumentException();
            _initialValue = initialValue;
            _currentValue = initialValue;
        }

        public void Increase(int value)
        {
            var temp = _currentValue;
            _currentValue += value;

            if (temp != _currentValue)
                OnValueChanged?.Invoke(_currentValue);
        }

        public void Decrease(int value)
        {
            var temp = _currentValue;
            _currentValue = Mathf.Max(_currentValue - value, 0);

            if (temp != _currentValue)
                OnValueChanged?.Invoke(_currentValue);
        }
    }
}