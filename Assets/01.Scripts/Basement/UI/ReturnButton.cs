using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Events;

namespace Basement
{
    public class ReturnButton : MonoBehaviour
    {
        [SerializeField] private Button _returnButton;
        private RectTransform buttonRect => _returnButton.transform as RectTransform;
        private Tween _tween;

        public void ChangeReturnAction(UnityAction action)
        {
            _returnButton.onClick.RemoveAllListeners();
            _returnButton.onClick.AddListener(action);
        }

        public void AddReturnAction(UnityAction action)
            => _returnButton.onClick.AddListener(action);

        public void RemoveReturnAction(UnityAction action)
            => _returnButton.onClick.RemoveListener(action);

        public void RemoveAllAction()
            => _returnButton.onClick.RemoveAllListeners();


        public void Open()
        {
            if (_tween != null && _tween.active) 
                _tween.Kill();

            _tween = buttonRect.DOAnchorPosX(40, 0.2f);
        }

        public void Close()
        {
            if (_returnButton.onClick.GetPersistentEventCount() > 0) return;

            if (_tween != null && _tween.active) 
                _tween.Kill();

            _tween = buttonRect.DOAnchorPosX(-40,0.2f);
        }
    }
}
