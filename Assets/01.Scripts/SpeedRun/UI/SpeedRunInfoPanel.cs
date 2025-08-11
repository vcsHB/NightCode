using SpeedRun;
using UnityEngine;
namespace UI.SpeedRun
{

    public class SpeedRunInfoPanel : MonoBehaviour
    {
        [SerializeField] private SpeedRunRecordGroup _speedRunRecordGroup;
        [SerializeField] private SpeedRunRankingSlot _rankingSlotPrefab;
        [SerializeField] private Transform _contentTrm;

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
            _speedRunRecordGroup.records.Sort((a, b) => a.time.CompareTo(b.time));

            for (int i = 0; i < _speedRunRecordGroup.records.Count; i++)
            {
                var record = _speedRunRecordGroup.records[i];
                SpeedRunRankingSlot rankingSlot = Instantiate(_rankingSlotPrefab, _contentTrm);
                rankingSlot.SetSpeedRunRecord(record, i);
            }
        }
    }
}