using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(menuName = "SO/Stat/StatusGroup")]
    public class StatGroupSO : ScriptableObject
    {
        public List<StatSO> statList;

        public bool TryGetStat(StatusEnumType statusType, out StatSO stat)
        {
            stat = statList.Find(stat => stat.statType == statusType);
            return stat != null;
        }
    }
}
