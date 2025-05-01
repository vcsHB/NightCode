using Dialog;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Office
{
    [CreateAssetMenu(menuName = "SO/OfficeNPC")]
    public class OfficeNPCSO : ScriptableObject
    {
        public List<NPCTalkStruct> dialogByTalkCount = new();
        private Comparison<NPCTalkStruct> compare = new Comparison<NPCTalkStruct>((a, b) => a.count.CompareTo(b.count));

        private void OnValidate()
        {
            dialogByTalkCount.Sort(compare);
        }


        public DialogSO GetDialog(int talkCount)
        {
            for (int i = 0; i < dialogByTalkCount.Count; i++)
            {
                if (dialogByTalkCount[i].count > talkCount)
                    return dialogByTalkCount[i].dialog;
            }

            return dialogByTalkCount[^1].dialog;
        }
    }

    [Serializable]
    public struct NPCTalkStruct
    {
        public DialogSO dialog;
        public int count;
    }
}
