using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public List<ChipsetSO> containChipset;      //코드상에서 제어할 수 있도록 변경해야함
        public ChipsetInventory inventory;          //나중에 3개로 늘려서 전환하는 방식으로 바꿔야함
        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;
        private Chipset _chipsetTemp;
        public void Init()
        {
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();
            containChipset.ForEach(AddChipset);
        }

        public void SetInventory(ChipsetInventory inventory)
        {
            this.inventory = inventory;
            _chipsetInfos.Keys.ToList().ForEach(chipset =>
            {
                inventory.AddAssignedChipset(chipset);
                _chipsetInfos[chipset].Init(inventory, chipset);
            });
        }

        public void AddChipset(ChipsetSO chipsetSO)
        {
            Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
            if (inventory != null) inventory.AddAssignedChipset(chipset);

            ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
            chipsetInfo.Init(inventory, chipset);
            chipsetInfo.onInsertChipset += OnInsertChipset;
            _chipsetInfos.Add(chipset, chipsetInfo);
        }

        private void OnInsertChipset(Chipset chipset)
        {
            _chipsetInfos[chipset].onInsertChipset -= OnInsertChipset;
            _chipsetInfos.Remove(chipset);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inventory.onReturnChipset += SetAssignChipset;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventory.onReturnChipset -= SetAssignChipset;
        }

        private void SetAssignChipset()
        {
            if (inventory.SelectedChipset == null || inventory.SelectedChipset.IsForcePointerDown) return;
            inventory.SelectedChipset.SetPrevPosition(null);
            AddChipset(inventory.SelectedChipset.info);
            inventory.RemoveChipset(inventory.SelectedChipset, true);
        }
    }

}
