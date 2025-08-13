using System;
using System.Collections.Generic;
namespace SpeedRun
{
    [System.Serializable]
    public class SpeedRunRecordGroup
    {
        public List<SpeedRunRecord> records;


        public bool IsDuplicatedRecord(string name)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].challengerName == name)
                {
                    return true;
                }
            }
            return false;
        }
        public bool TryAddRecord(string name, float time)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].challengerName == name)
                {
                    if (records[i].playTime > time)
                        records[i].playTime = time;
                }
            }

            records.Add(new SpeedRunRecord()
            {
                challengerName = name,
                playTime = time,
                recordedTime = DateTime.Now.Second
            });
            return true;
        }
    }
}