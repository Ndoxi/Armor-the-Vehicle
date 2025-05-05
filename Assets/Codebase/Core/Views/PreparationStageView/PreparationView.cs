using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Core.Views
{
    public class PreparationView : MonoBehaviour, IStateView
    {
        private const string LogTag = "View";

        [SerializeField] private RectTransform _rootView;
        [SerializeField] private Button _finishPreparationsButton;
        private PreparationsController _preparationsController;
        private List<IAnimatedPanel> _panels;
        private ILogger _logger;

        [Inject]
        private void Construct(PreparationsController preparationsController, 
                               List<IAnimatedPanel> panels, 
                               ILogger logger)
        {
            _preparationsController = preparationsController;
            _panels = panels;
            _logger = logger;
        }

        private void Awake()
        {
            DisableView();
        }

        private void OnEnable()
        {
            _finishPreparationsButton.onClick.AddListener(FinishPreparations);
        }

        private void OnDisable()
        {
            _finishPreparationsButton.onClick.RemoveListener(FinishPreparations);
        }

        public void Show()
        {
            ShowAllAsync().Forget();
        }

        public void Hide()
        {
            HideAllAsync().Forget();
        }

        private void FinishPreparations()
        {
            _preparationsController.FinishPreparations();
        }

        private async UniTaskVoid ShowAllAsync()
        {
            EnableView();
            try
            {
                await UniTask.WhenAll(_panels.Select(panel => panel.Show()).ToArray());
            }
            catch (OperationCanceledException)
            {
                _logger.Log(LogTag, "Operation cancelled");
                DisableView();
            }
        }

        private async UniTaskVoid HideAllAsync() 
        {
            try
            {
                await UniTask.WhenAll(_panels.Select(panel => panel.Hide()).ToArray());
            }
            catch (OperationCanceledException)
            {
                _logger.Log(LogTag, "Operation cancelled");
            }
            finally
            {
                DisableView();
            }
        }

        private void EnableView()
        {
            _rootView.gameObject.SetActive(true);
        }

        private void DisableView()
        {
            _rootView.gameObject.SetActive(false);
        }
    }
}
