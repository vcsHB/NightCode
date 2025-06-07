using Agents;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInfo : MonoBehaviour, IPointerDownHandler
    {
        public event Action<Chipset> onInsertChipset;
        [SerializeField] private Image _icon;
        private Chipset _assignedChipset;
        private ChipsetInventory _inventory;
        private float _scaleUpDuration = 0.3f;
        private Canvas _canvas;

        public RectTransform RectTrm => transform as RectTransform;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
        }

        public void Init(ChipsetInventory inventory, Chipset chipset)
        {
            _inventory = inventory;
            _assignedChipset = chipset;
            _assignedChipset.RectTrm.localScale = Vector3.zero;
        }

        public void OnInsertChipset()
        {
            onInsertChipset?.Invoke(_assignedChipset);
            RemoveAction();

            Destroy(gameObject);
        }

        public void OnReturnChipset()
        {
            _icon.gameObject.SetActive(true);
            _assignedChipset.RectTrm.localScale = Vector3.zero;
            RemoveAction();
        }

        private void RemoveAction()
        {
            _inventory.onInsertChipset -= OnInsertChipset;
            _inventory.onReturnChipset -= OnReturnChipset;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Camera uiCamera = _canvas.worldCamera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _assignedChipset.ParentRectTrm,
                Input.mousePosition,
                uiCamera,
                out Vector2 localPoint
            );

            _assignedChipset.RectTrm.localPosition = localPoint;

            _assignedChipset.ForceOnPointerDown(Vector2Int.zero, eventData);
            _assignedChipset.RectTrm.DOScale(1, _scaleUpDuration);
            _icon.gameObject.SetActive(false);

            _inventory.onInsertChipset += OnInsertChipset;
            _inventory.onReturnChipset += OnReturnChipset;
        }
    }
}
