using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class DialogConditionManager : MonoBehaviour
    {
        #region Singleton

        public static DialogConditionManager instance;
        public static bool destroyed = false;

        public static DialogConditionManager Instance
        {
            get
            {
                if (instance == null && !destroyed)
                {
                    instance = GameObject.FindAnyObjectByType<DialogConditionManager>();

                    if (instance == null)
                    {
                        Debug.LogError("DialogConditionManager is not Exsist");
                    }
                }
                return instance;
            }
        }

        public void OnDestroy()
        {
            destroyed = true;
        }

        #endregion

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
