using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogOptionObject : MonoBehaviour
    {
        internal Action onSelect;
        [SerializeField] private TextMeshProUGUI _tmp;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetOption(Option option)
        {
            _tmp.SetText(option.optionTxt);
        }

        public void OnSelectOption()
        {
            onSelect?.Invoke();
        }

        public void OnHover(bool isHover)
        {
            _canvasGroup.alpha = isHover ? 1.0f : 0.7f;
            transform.localScale = Vector3.one * (isHover ? 1.05f : 1.0f);
        }
    }
}
