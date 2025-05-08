using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace Base.Cafe
{
    public class NumberPassing : MonoBehaviour
    {
        [SerializeField] private RectTransform _maskRect;
        [SerializeField] private TextMeshProUGUI _tmp1, _tmp2;
        [SerializeField] private Vector2 _textSize;
        public float duration;

        private float _speed;
        private string _format;
        private bool _isFirstText = true;
        private bool _isStartAnim = false;

        private int _targetNumber;
        private int _currentNumber;
        private string _numberText;

        private void Update()
        {
            if (_isStartAnim)
            {
                _tmp1.rectTransform.anchoredPosition += Vector2.up * _speed * Time.deltaTime;
                _tmp2.rectTransform.anchoredPosition += Vector2.up * _speed * Time.deltaTime;

                if (_isFirstText) CheckText(_tmp1);
                else CheckText(_tmp2);
            }

            if (_currentNumber == _targetNumber)
            {
                _isStartAnim = false;

                _tmp1.rectTransform.anchoredPosition += Vector2.up * _speed * Time.deltaTime;
                _tmp2.rectTransform.anchoredPosition += Vector2.up * _speed * Time.deltaTime;

                if (_isFirstText && _tmp2.rectTransform.anchoredPosition.y >= 0)
                {
                    _tmp2.rectTransform.anchoredPosition = Vector2.zero;
                    _tmp1.rectTransform.anchoredPosition = Vector2.up * _textSize.y;
                    _targetNumber++;
                }
                if (!_isFirstText && _tmp1.rectTransform.anchoredPosition.y >= 0)
                {
                    _tmp1.rectTransform.anchoredPosition = Vector2.zero;
                    _tmp2.rectTransform.anchoredPosition = Vector2.up * _textSize.y;
                    _targetNumber++;
                }
            }
        }

        private void CheckText(TextMeshProUGUI tmp)
        {
            if (tmp.rectTransform.anchoredPosition.y >= _textSize.y)
            {
                tmp.rectTransform.anchoredPosition = new Vector2(0, -_textSize.y);

                _currentNumber++;
                _numberText = _currentNumber.ToString(_format);
                tmp.SetText(_numberText);
            }

            _isFirstText = !_isFirstText;
        }

        public void SetText(int number)
        {
            _isStartAnim = true;
            _targetNumber = number;
            _speed = (number * _textSize.y) / duration;

            int digit = 1;
            while (number > 10)
            {
                number /= 10;
                digit++;
            }

            _format = $"D{digit}";

            _currentNumber = 0;
            _numberText = _currentNumber.ToString(_format);
            _tmp1.SetText(_numberText);
            _tmp1.rectTransform.anchoredPosition = new Vector2(0, 0);

            _currentNumber = 1;
            _numberText = _currentNumber.ToString(_format);
            _tmp2.SetText(_numberText);
            _tmp2.rectTransform.anchoredPosition = new Vector2(0, -_textSize.y);

            _maskRect.sizeDelta = new Vector2(_textSize.x * digit, _textSize.y);
        }
    }
}
