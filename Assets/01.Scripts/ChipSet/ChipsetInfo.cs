using Agents;
using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInfo : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Chipset> onInsertChipset;
        public event Action<ChipsetSO> onEnter;
        public event Action onExit;

        [SerializeField] private Image _icon;
        private Chipset _assignedChipset;
        private ChipsetInventory _inventory;
        private float _scaleUpDuration = 0.3f;
        private Tween _chipsetScaleTween;

        public RectTransform RectTrm => transform as RectTransform;

        public void Init(ChipsetInventory inventory, Chipset chipset)
        {
            _inventory = inventory;
            _assignedChipset = chipset;
            _assignedChipset.RectTrm.localScale = Vector3.zero;
            _icon.sprite = chipset.info.icon;
        }

        public void OnInsertChipset()
        {
            onInsertChipset?.Invoke(_assignedChipset);
            RemoveAction();

            SetActive(false);
        }

        public void OnReturnChipset()
        {
            _inventory.OnPointerDownChipset(-1);

            if (_chipsetScaleTween != null) _chipsetScaleTween.Kill();
            _assignedChipset.RectTrm.localScale = Vector3.zero;

            _icon.gameObject.SetActive(true);
            RemoveAction();
        }

        private void RemoveAction()
        {
            _inventory.onInsertChipset -= OnInsertChipset;
            _inventory.onReturnChipset -= OnReturnChipset;
        }


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
            
            _inventory.onInsertChipset += OnInsertChipset;
            _inventory.onReturnChipset += OnReturnChipset;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke(_assignedChipset.info);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            
            if (isActive == false) onExit?.Invoke();
            else _icon.gameObject.SetActive(true);
        }
    }
}
