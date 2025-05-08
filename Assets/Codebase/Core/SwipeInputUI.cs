using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.Core
{
    public class SwipeInputUI : MonoBehaviour, IDragHandler, IEndDragHandler, ISwipeInput
    {
        [SerializeField] private float _swipeSensitivity = 1f;
        [SerializeField] private float _maxSwipeDelta = 10f;
        private float _horizontalDelta;

        public void OnDrag(PointerEventData eventData)
        {
            _horizontalDelta = Mathf.Clamp(eventData.delta.x / _maxSwipeDelta, -1f, 1f) * _swipeSensitivity;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _horizontalDelta = 0f;
        }

        public float GetHorizontal()
        {
            return _horizontalDelta;
        }
    }
}