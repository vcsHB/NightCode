using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class DialogConditionManager : MonoSingleton<DialogConditionManager>
    {
        public Dictionary<string, int> visit = new Dictionary<string, int>();
        public int coin;

        public int GetVisit(string nodeGuid)
        {
            if (visit.TryGetValue(nodeGuid, out int visitCnt))
                return visitCnt;

            return 0;
        }

        public void CountVisit(string nodeGuid)
        {
            if (visit.TryGetValue(nodeGuid, out int cnt))
            {
                visit[nodeGuid] = cnt + 1;
                return;
            }

            visit.Add(nodeGuid, 1);
        }
    }
}
