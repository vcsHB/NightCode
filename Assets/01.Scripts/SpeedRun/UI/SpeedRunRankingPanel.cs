using SpeedRun;
using UnityEngine;
namespace UI.SpeedRun
{

    public class SpeedRunRankingPanel : MonoBehaviour
    {
        [SerializeField] private SpeedRunDataController _dataController;
        [SerializeField] private SpeedRunRankingSlot _rankingSlotPrefab;
        [SerializeField] private Transform _contentTrm;

        private void Start()
        {
            RefreshRecords();
        }

        private void DeleteAllRankingSlots()
        {
            foreach (Transform childSlot in _contentTrm)
            {
                Destroy(childSlot.gameObject);

            }
        }

        [ContextMenu("DebugRefreshRecords")]
        public void RefreshRecords()
        {
            DeleteAllRankingSlots();

            // time이 작은 순으로 정렬
            _dataController.RecordData.records.Sort((a, b) => a.playTime.CompareTo(b.playTime));

            for (int i = 0; i < _dataController.RecordData.records.Count; i++)
            {
                var record = _dataController.RecordData.records[i];
                SpeedRunRankingSlot rankingSlot = Instantiate(_rankingSlotPrefab, _contentTrm);
                rankingSlot.SetSpeedRunRecord(record, i);
            }
        }
    }
}