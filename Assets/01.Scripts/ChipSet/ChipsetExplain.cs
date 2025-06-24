using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetExplain : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _explainText;
        [SerializeField] private Image _iconImage;
        [SerializeField]private CanvasGroup _canvasGroup;

        private RectTransform RectTrm => transform as RectTransform;

        public void SetChipsetExplain(ChipsetSO chipset)
        {
            _canvasGroup.alpha = 1;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((transform.parent as RectTransform), Mouse.current.position.value, Camera.main, out Vector2 localPosition);
            RectTrm.localPosition = localPosition; 

            _nameText.SetText(chipset.chipsetName);
            _explainText.SetText(chipset.chipsetName);
            _iconImage.sprite = chipset.icon;
        }

        public void Disable()
        {
            _canvasGroup.alpha = 0;
        }

    }
}
