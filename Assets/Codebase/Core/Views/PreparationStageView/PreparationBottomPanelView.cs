using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Threading;

namespace Codebase.Core.Views
{
    public class PreparationBottomPanelView : MonoBehaviour, IAnimatedPanel
    {
        [SerializeField] private RectTransform _rootView;
        [SerializeField] private float _animationDuration = 1f;
        private Vector3 _initialLocalPosition;
        private Vector3 _hiddenLocalPosition;
        private CancellationTokenSource _cancellationTokenSourceOnDisable;

        private void Awake()
        {
            InitPositions();
        }

        private void OnEnable()
        {
            _cancellationTokenSourceOnDisable = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancellationTokenSourceOnDisable?.Cancel();
            _cancellationTokenSourceOnDisable?.Dispose();
        }

        public UniTask Show()
        {
            return DoLocalMoveAsync(_initialLocalPosition, _animationDuration);
        }

        public UniTask Hide()
        {
            return DoLocalMoveAsync(_hiddenLocalPosition, _animationDuration);
        }

        private void InitPositions()
        {
            _initialLocalPosition = _rootView.localPosition;

            var tempPos = _rootView.localPosition;
            tempPos.y -= _rootView.rect.height;
            _hiddenLocalPosition = tempPos;

            _rootView.localPosition = _hiddenLocalPosition;
        }

        private UniTask DoLocalMoveAsync(Vector3 endPosition, float duration)
        {
            var completionSource = new UniTaskCompletionSource();
            var calcellationToken = _cancellationTokenSourceOnDisable.Token;
            calcellationToken.Register(() => completionSource.TrySetCanceled(calcellationToken));

            _rootView.DOLocalMove(endPosition, duration)
                     .OnComplete(() => completionSource.TrySetResult());

            return completionSource.Task;
        }
    }
}