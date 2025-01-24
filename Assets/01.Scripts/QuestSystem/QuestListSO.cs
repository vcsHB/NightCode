using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{

    [CreateAssetMenu(menuName = "SO/QuestListSO")]
    public class QuestListSO : ScriptableObject
    {
        [field: SerializeField] public List<QuestSO> quests { get; private set; }

        public QuestSO GetQuest(int id)
        {
            for (int i = 0; i < quests.Count; i++)
            {
                if(quests[i].id == id)
                    return quests[i];
            }
            return null;
        }
    }
}