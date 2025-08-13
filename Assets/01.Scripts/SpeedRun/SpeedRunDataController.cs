using System.IO;
using UnityEngine;

namespace SpeedRun
{
    public class SpeedRunDataController : MonoBehaviour
    {
        [SerializeField] private SpeedRunRecordGroup _speedRunRecordGroup;
        public SpeedRunRecordGroup RecordData => _speedRunRecordGroup;

        private static string _speedRunSavePath = Path.Combine(Application.dataPath, "Save/SpeedRunData.json");

        private SpeedRunRecord _newRecord;

        private void Awake()
        {
            Load();
        }

        private void OnDestroy()
        {
            Save();
        }

        public void Save()
        {
            try
            {
                string directory = Path.GetDirectoryName(_speedRunSavePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                string json = JsonUtility.ToJson(_speedRunRecordGroup, true);
                File.WriteAllText(_speedRunSavePath, json);

            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SpeedRunDataController] Save Failed: {ex}");
            }
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_speedRunSavePath))
                {
                    string json = File.ReadAllText(_speedRunSavePath);
                    _speedRunRecordGroup = JsonUtility.FromJson<SpeedRunRecordGroup>(json);
                }
                else
                {
                    _speedRunRecordGroup = new SpeedRunRecordGroup();
                    Save();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[SpeedRunDataController] Load Failed: {ex}");
                _speedRunRecordGroup = new SpeedRunRecordGroup();
            }
        }

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
