using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;

namespace Basement
{
    public class MSGTextBox : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _text;
        private Tween _tween;

        public void Init(Sprite icon, string text)
        {
            if (icon == null)
                _icon.gameObject.SetActive(false);
            else
                _icon.sprite = icon;

            _text.SetText(text);
        }
    }
}
