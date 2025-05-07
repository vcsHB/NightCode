using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Cafe
{

    public class MSGTextBox : MonoBehaviour
    {
        public RectTransform rectTrm => transform as RectTransform;
        public RectTransform childRect => transform.GetChild(0) as RectTransform;

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _fadeStartDelay = 0.3f;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private float _upSpeed = 100;

        [Space]
        [SerializeField] private GameObject _ratingParent;
        [SerializeField] private List<GameObject> _ratingObjects;


        private RectTransform _raingParnetTrm => _ratingParent.transform as RectTransform;

        private MSGText _msgText;
        private MSGTextBox _prevTextBox;
        private CanvasGroup _canvasGroup;
        private Sequence _seq;
        private bool _isStartFade = false;

        private bool _timerStart = false;
        private float _timer;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_timerStart && _timer + _fadeStartDelay < Time.time && !_isStartFade)
            {
                _isStartFade = true;
                _timer = Time.time;
            }

            if (_isStartFade)
            {
                rectTrm.anchoredPosition += Vector2.up * _upSpeed * Time.deltaTime;
                _canvasGroup.alpha -= (1 / _fadeDuration) * Time.deltaTime;

                if (_timer + _fadeDuration < Time.time)
                    _msgText.Push();
            }
        }

        public void Init(Sprite icon, string text, MSGText msgText, MSGTextBox prevTextBox, int rating)
        {
            _isStartFade = false;
            _timerStart = false;

            if (icon == null)
            {
                _icon.gameObject.SetActive(false);
            }
            else
            {
                _icon.gameObject.SetActive(true);
                _icon.sprite = icon;
            }

            _msgText = msgText;
            _text.SetText(text);
            if (prevTextBox != this) _prevTextBox = prevTextBox;

            rectTrm.anchoredPosition = new Vector2(-childRect.rect.width, 0);
            _canvasGroup.alpha = 1;

            if (_prevTextBox != null && _prevTextBox.gameObject.activeSelf)
            {
                float height = rectTrm.anchoredPosition.y + childRect.rect.height + 5;

                if (_prevTextBox.rectTrm.anchoredPosition.y < height)
                    _prevTextBox.MoveUp(height);
            }

            if (_seq != null && _seq.active)
                _seq.Kill();

            _seq = DOTween.Sequence();

            _seq.AppendInterval(0.05f)
                .Append(rectTrm.DOAnchorPosX(0, 0.2f))
                .OnComplete(() =>
                {
                    _timerStart = true;
                    _timer = Time.time;
                });

            _ratingParent.SetActive(rating > 0);
            for (int i = 0; i < _ratingObjects.Count; i++)
            {
                _ratingObjects[i].SetActive(i < rating);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(childRect);
                LayoutRebuilder.ForceRebuildLayoutImmediate(_raingParnetTrm);
        }

        public void MoveUp(float height)
        {
            if (_isStartFade == false)
                _seq.Complete();

            if (rectTrm.anchoredPosition.y < height)
                rectTrm.DOAnchorPosY(height, 0.05f);

            if (_prevTextBox != null && _prevTextBox.gameObject.activeSelf)
            {
                height += childRect.rect.height + 5;

                if (_prevTextBox.rectTrm.anchoredPosition.y < height)
                {
                    _prevTextBox.MoveUp(height);
                }
            }
        }

        public void StartFade()
        {
            _isStartFade = true;
            _timer = Time.time;
        }
    }
}
