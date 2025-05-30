using System.Collections.Generic;
using UnityEngine;

namespace Chipset
{
    public class ChipsetContainer : MonoBehaviour
    {
        public List<ChipsetSO> containChipset;      //�ڵ�󿡼� ������ �� �ֵ��� �����ؾ���
        public ChipsetInventory inventory;          //���߿� 3���� �÷��� ��ȯ�ϴ� ������� �ٲ����
        [SerializeField] private ChipsetInfo _chipsetInfoPrefab;
        [Space]
        [SerializeField] private Transform _chipsetParent;
        [SerializeField] private Transform _chipsetInfoParent;
        private List<ChipsetInfo> _chipsetInfos;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _chipsetInfos = new List<ChipsetInfo>();
            containChipset.ForEach(chipsetSO =>
            {
                Chipset chipset = Instantiate(chipsetSO.chipsetPrefab, _chipsetParent);
                inventory.AddAssignedChipset(chipset);
                inventory.Init();

                ChipsetInfo chipsetInfo = Instantiate(_chipsetInfoPrefab, _chipsetInfoParent);
                chipsetInfo.Init(inventory, chipset);
                _chipsetInfos.Add(chipsetInfo);

                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, chipsetInfo.RectTrm.anchoredPosition);

                RectTransformUtility.ScreenPointToLocalPointInRectangle((_chipsetParent as RectTransform), screenPoint, null, out Vector2 localPoint);
                chipset.RectTrm.anchoredPosition = localPoint;
            });
        }
    }
}
