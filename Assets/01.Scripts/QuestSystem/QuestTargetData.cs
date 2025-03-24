using UnityEngine;
namespace QuestSystem
{
    [System.Serializable]
    public struct QuestTargetData
    {
        public string targetCode;
        public float completeLevel; // Progress Increasing Value 
    }
}