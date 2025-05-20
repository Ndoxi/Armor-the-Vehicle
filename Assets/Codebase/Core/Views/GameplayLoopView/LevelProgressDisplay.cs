using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.Views
{
    public class LevelProgressDisplay : MonoBehaviour
    {
        private static string LabelText = "{0}m";

        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textMesh;
        private LevelProgress _current;

        private void OnEnable()
        {
            if (_current != null)
            {
                _current.OnValueUpdate += UpdateValue;
                Initialize();
            }
        }

        private void OnDisable()
        {
            if (_current != null)
                _current.OnValueUpdate -= UpdateValue;
        }

        public void SetProgress(LevelProgress progress)
        {
            if (_current != null)
                _current.OnValueUpdate -= UpdateValue;
            _current = progress;
            _current.OnValueUpdate += UpdateValue;

            Initialize();
        }

        private void Initialize()
        {
            UpdateValue(0);
        }

        private void UpdateValue(int _)
        {
            float fillVlaue = (float)_current.CurrentValue / _current.TargetValue;
            _slider.value = fillVlaue;
            _textMesh.text = string.Format(LabelText, _current.CurrentValue);
        }
    }
}