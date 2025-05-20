using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.Views
{
    public class GameplayLoopStateView : MonoBehaviour, IStateView
    {
        public event Action OnReset;

        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _completedRoot;
        [SerializeField] private RectTransform _failRoot;
        [SerializeField] private LevelProgressDisplay _levelProgressDisplay;
        [SerializeField] private Button _resetLevelButton;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            _resetLevelButton.onClick.AddListener(RequestLevelReset);
        }

        private void OnDisable()
        {
            _resetLevelButton.onClick.RemoveListener(RequestLevelReset);
        }

        public void Show()
        {
            _root.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _root.gameObject.SetActive(false);
        }

        public void ResetUI()
        {
            _completedRoot.gameObject.SetActive(false);
            _failRoot.gameObject.SetActive(false);
            _resetLevelButton.gameObject.SetActive(false);
        }

        public void SetProgress(LevelProgress progress)
        {
            _levelProgressDisplay.SetProgress(progress);
        }

        public void OnLevelCompleted(LevelProgressChecker.State result)
        {
            _resetLevelButton.gameObject.SetActive(true);

            switch (result)
            {
                case LevelProgressChecker.State.Completed:
                    _completedRoot.gameObject.SetActive(true);
                    break;
                case LevelProgressChecker.State.Failed:
                    _failRoot.gameObject.SetActive(true);
                    break;
            }
        }

        private void Init()
        {
            _root.gameObject.SetActive(false);
            ResetUI();
        }

        private void RequestLevelReset()
        {
            OnReset?.Invoke();
        }
    }
}