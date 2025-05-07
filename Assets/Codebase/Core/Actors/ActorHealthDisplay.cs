using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Core.Actors
{
    [RequireComponent(typeof(Canvas))]
    public class ActorHealthDisplay : MonoBehaviour
    {
        [SerializeField] private bool _autoHide;
        [SerializeField] private Image _fillImage;
        protected Canvas _canvas;
        protected Actor _actor;

        [Inject]
        private void Construct(Actor actor)
        {
            _actor = actor;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        protected virtual void OnEnable()
        {
            UpdateDisplay();
            _actor.Health.OnValueChanged += UpdateDisplay;
        }

        protected virtual void OnDisable()
        {
            _actor.Health.OnValueChanged -= UpdateDisplay;
        }

        private void UpdateDisplay(int _ = 0)
        {
            if (_actor.Health.Current <= 0 
                || (_autoHide && _actor.Health.Current == _actor.Health.Initial))
            {
                _canvas.enabled = false;
                return;
            }

            _canvas.enabled = true;
            _fillImage.fillAmount = (float)_actor.Health.Current / _actor.Health.Initial;
        }
    }
}