using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInfo : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
    {
        public event Action<Chipset> onInsertChipset;
        public event Action<Chipset> onReturnChipset;
        public event Action<ChipsetInfo> onSelectInventory;
        public event Action<ChipsetInfo> onUnSelectInventory;
        public event Action<ChipsetSO> onSetExplain;
        public event Action onRemoveExplain;

        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        private Chipset _assignedChipset;
        private float _scaleUpDuration = 0.3f;
        private Tween _chipsetScaleTween;

        public RectTransform RectTrm => transform as RectTransform;

        public void Init(Chipset chipset)
        {
            _assignedChipset = chipset;
            _assignedChipset.RectTrm.localScale = Vector3.zero;
            _icon.sprite = chipset.info.icon;
            _nameText.SetText(chipset.info.chipsetName);
        }

        public void OnInsertChipset()
        {
            onInsertChipset?.Invoke(_assignedChipset);
            onUnSelectInventory?.Invoke(this);
            
            SetActive(false);
        }

        public void OnReturnChipset()
        {
            onReturnChipset?.Invoke(_assignedChipset);
            
            if (_chipsetScaleTween != null) _chipsetScaleTween.Kill();
            _assignedChipset.RectTrm.localScale = Vector3.zero;

            _icon.gameObject.SetActive(true);
            onUnSelectInventory?.Invoke(this);
        }

        public void RemoveAction(ChipsetInventory inventory)
        {
            inventory.onInsertChipset -= OnInsertChipset;
            inventory.onReturnChipset -= OnReturnChipset;
        }

        public void AddAction(ChipsetInventory inventory)
        {
            inventory.onInsertChipset += OnInsertChipset;
            inventory.onReturnChipset += OnReturnChipset;
        }


        #region EventRegion
        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _assignedChipset.ParentRectTrm,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );

            _assignedChipset.RectTrm.localPosition = localPoint;
            _assignedChipset.ForceOnPointerDown(Vector2Int.zero, eventData);

            if (_chipsetScaleTween != null && _chipsetScaleTween.active)
                _chipsetScaleTween.Kill();
            _chipsetScaleTween = _assignedChipset.RectTrm.DOScale(1, _scaleUpDuration);

            _icon.gameObject.SetActive(false);
            onSelectInventory?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onSetExplain?.Invoke(_assignedChipset.info);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onRemoveExplain?.Invoke();
        }

        #endregion

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);

            if (isActive == false) onRemoveExplain?.Invoke();
            else _icon.gameObject.SetActive(true);
        }
    }
}
