using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.InGame.SystemUI.AlertSystem
{

    public class AlertBox : MonoBehaviour
    {
        public event Action<AlertBox> OnDisableEvent;
        [SerializeField] private TextMeshProUGUI _contentText;
        private RectTransform _rectTrm;
        [SerializeField] private float _displayDuration;
        [SerializeField] private float _disableDuration;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTrm = transform as RectTransform;
        }
        public void SetPos(Vector2 rectPosition)
        {
            _rectTrm.anchoredPosition = rectPosition;
        }

        public void SetAlert(string content)
        {
            _contentText.text = content;
            _canvasGroup.alpha = 1f;
            StartCoroutine(AlertCoroutine());
        }
        private IEnumerator AlertCoroutine()
        {
            yield return new WaitForSeconds(_displayDuration);

            _canvasGroup.DOFade(0f, _disableDuration);
            _rectTrm.DOAnchorPosY(300f, _disableDuration);
            yield return new WaitForSeconds(_disableDuration);
            OnDisableEvent?.Invoke(this);
        }
    }

}