using System;
using JetBrains.Annotations;
using UnityEngine;
namespace QuestSystem.QuestTarget
{

    [CreateAssetMenu(menuName = "SO/TargetInfoSO")]
    public class TargetInfoSO : ScriptableObject
    {
        public string targetName; // use for code
        public Sprite targetIcon;
        private int hash;

        private void OnValidate()
        {
            hash = targetName.GetHashCode();
            
        }




    }
}