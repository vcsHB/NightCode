using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour
    {
        public List<ChipsetSO> containChipset;      //�ڵ�󿡼� ������ �� �ֵ��� �����ؾ���
        public ChipsetInventory inventory;          //���߿� 3���� �÷��� ��ȯ�ϴ� ������� �ٲ����
        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private RectTransform _chipsetParent;
        [SerializeField] private RectTransform _chipsetInfoParent;
        private Dictionary<Chipset, ChipsetInfo> _chipsetInfos;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _chipsetInfos = new Dictionary<Chipset, ChipsetInfo>();
            List<Chipset> chipsets = new List<Chipset>();
            containChipset.ForEach(chipsetSO =>
            {
                Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
                chipsets.Add(chipset);
                inventory.AddAssignedChipset(chipset);
                inventory.Init();

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(inventory, chipset);
                _chipsetInfos.Add(chipset, chipsetInfo);
            });
        }
    }

}
