using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class UIPopupText : MonoBehaviour
{
    private TextMeshProUGUI _tmp;
    private RectTransform _rectTrm => transform as RectTransform;

    private Color _textColor = Color.white;
    private float _textMoveValue = 0.5f;
    private float _easingTime = 0.35f;
    private float _textScale = 1f;

    private Sequence _seq;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        transform.SetAsLastSibling();
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            SetText("str +2", Color.red, 50f, 0.4f, 1f, Input.mousePosition);
        }
    }


    public void SetText(string text, Color textColor, float textMoveValue, float easingTime, float textScale, Vector2 textPosition)
    {
        _textColor = textColor;
        _textMoveValue = textMoveValue;
        _easingTime = easingTime;
        _textScale = textScale;

        _tmp.text = text;
        _tmp.color = _textColor;
        _rectTrm.localScale = Vector3.one * _textScale;
        _rectTrm.anchoredPosition = textPosition;

        if (_seq != null && _seq.active) _seq.Kill();
        _seq = DOTween.Sequence();

        float targetPos = textPosition.y + _textMoveValue;
        _seq.Append(_rectTrm.DOAnchorPosY(targetPos, _easingTime))
            .Join(_tmp.DOFade(0, _easingTime))
            .OnComplete(() => Destroy(gameObject));
    }


    public void SetText(string text)
    {
        _tmp.text = text;
        _tmp.color = _textColor;
        _rectTrm.localScale = Vector3.one * _textScale;

        if (_seq != null && _seq.active) _seq.Kill();
        _seq = DOTween.Sequence();

        float targetPos = _rectTrm.anchoredPosition.y + _textMoveValue;
        _seq.Append(_rectTrm.DOAnchorPosY(targetPos, _easingTime))
            .Join(_tmp.DOFade(0, _easingTime))
            .OnComplete(() => Destroy(gameObject));
    }
}
