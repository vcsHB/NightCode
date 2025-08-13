using UnityEngine;
namespace SpeedRun
{

    public class SpeedRunDataController : MonoBehaviour
    {
        [SerializeField] private SpeedRunRecordGroup _speedRunRecordGroup;
        public SpeedRunRecordGroup RecordData => _speedRunRecordGroup;

        private SpeedRunRecord _newRecord;
        //TODO  : SAVE LOAD

        public void InitializeChallenger(string name)
        {
            _newRecord = new()
            {
                challengerName = name
            };
            _speedRunRecordGroup.records.Add(_newRecord);
        }

        public void RecordSpeedRunTime(float time)
        {
            _newRecord.playTime = time;
            
        }
    }
}