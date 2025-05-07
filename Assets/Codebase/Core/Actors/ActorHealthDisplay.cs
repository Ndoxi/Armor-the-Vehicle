using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(Canvas))]
    public abstract class ActorHealthDisplay : MonoBehaviour
    {
        protected abstract Actor Actor { get; }

        [SerializeField] private bool _autoHide;
        [SerializeField] private Image _fillImage;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            UpdateDisplay();
            Actor.Health.OnValueChanged += UpdateDisplay;
        }

        private void OnDisable()
        {
            Actor.Health.OnValueChanged -= UpdateDisplay;
        }

        private void UpdateDisplay(int _ = 0)
        {
            if (Actor.Health.Current <= 0 
                || (_autoHide && Actor.Health.Current == Actor.Health.Initial))
            {
                _canvas.enabled = false;
                return;
            }

            _canvas.enabled = true;
            _fillImage.fillAmount = (float)Actor.Health.Current / Actor.Health.Initial;
        }
    }
}