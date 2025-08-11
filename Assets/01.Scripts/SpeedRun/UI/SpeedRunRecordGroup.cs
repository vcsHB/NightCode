using System;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
namespace SpeedRun
{
    [System.Serializable]
    public class SpeedRunRecordGroup
    {
        public List<SpeedRunRecord> records;


        public void IsDuplicatedRecord(string name)
        {

        }
        public bool TryAddRecord(string name, float time)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].challengerName == name)
                {
                    if (records[i].time > time)
                        records[i].time = time;
                }
            }

            records.Add(new SpeedRunRecord()
            {
                challengerName = name,
                time = time,
                recordedTime = DateTime.Now.Second
            });
            return true;
        }
    }
}