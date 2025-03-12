using System;
using System.Collections.Generic;
using QuestSystem.QuestTarget;
using UnityEngine;
namespace QuestSystem.LevelSystem
{

    public class QuestTargetGroup : MonoBehaviour
    {
        public List<QuestTargetObject> questTargetList;

        public void AddQuestHandler(Action<QuestTargetData> completeHandle)
        {
            for (int i = 0; i < questTargetList.Count; i++)
            {
                questTargetList[i].OnTargetCompleteEvent += completeHandle;
            }
        }


        
    }
}