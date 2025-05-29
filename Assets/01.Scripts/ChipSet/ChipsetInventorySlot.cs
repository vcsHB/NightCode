using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chipset
{
    public class ChipsetInventorySlot : ChipsetSlot
    {
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _chipsetInsertableColor, _chipsetNotInsertableColor;
        [SerializeField] private Color _selectionColor;
        [SerializeField] private float _disableAlpha;
        private bool _isEnableSlot = true;

        public bool IsEnableSlot => _isEnableSlot;

        public void CheckChipsetInsertable(bool isEnable, bool insertable)
        {
            if (isEnable)
            {
                _image.color = insertable ? _chipsetInsertableColor : _chipsetNotInsertableColor;
            }
            else
            {
                _image.color = _normalColor;
            }
        }

        public void SetSelection(bool isEnable)
        {
            _image.color = isEnable ? _selectionColor : _normalColor;
        }

        #region SetEnables

        public void SetEnableSlot(bool isEnable)
        {
            if (isEnable) EnableSlot();
            else DisableSlot();
        }

        private void EnableSlot()
        {
            _isEnableSlot = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        private void DisableSlot()
        {
            _isEnableSlot = false;
            _canvasGroup.alpha = _disableAlpha;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }


        #endregion
    }
}
