using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetInfo : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _icon;
        private Chipset _assignedChipset;
        private ChipsetInventory _inventory;
        private float _scaleUpDuration = 0.3f;

        public RectTransform RectTrm => transform as RectTransform;

        public void Init(ChipsetInventory inventory, Chipset chipset)
        {
            _inventory = inventory;
            _assignedChipset = chipset;
            _assignedChipset.RectTrm.localScale = Vector3.zero;
        }

        public void OnInsertChipset()
        {
            Destroy(gameObject);
            RemoveAction();
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
            _assignedChipset.OnPointerDown(_assignedChipset.info.chipsetSize[0]);
            _assignedChipset.RectTrm.DOScale(1, _scaleUpDuration);
            _icon.gameObject.SetActive(false);

            _inventory.onInsertChipset += OnInsertChipset;
            _inventory.onReturnChipset += OnReturnChipset;
        }
    }
}
