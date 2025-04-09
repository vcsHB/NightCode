using StatSystem;
using System;
using UnityEngine;

namespace Office.CharacterSkillTree
{
    [CreateAssetMenu(menuName = "SO/StatIncreaseNode")]
    public class StatIncNodeSO : NodeSO
    {
        public StatIncrease[] stat;
    }

    [Serializable]
    public struct StatIncrease
    {
        public StatusEnumType statType;
        public float increaseValue;
    }
}

