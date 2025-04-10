using Office.CharacterSkillTree;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;


namespace StatSystem
{
    public class CharacterStatManager : MonoSingleton<CharacterStatManager>
    {
        private Dictionary<CharacterEnum, StatGroupSO> statGroup;
        public Dictionary<CharacterEnum, StatGroupSO> StatGroup
        {
            get
            {
                if (statGroup == null) InitDict();
                return statGroup;
            }
            private set
            {
                statGroup = value;
            }
        }

        public StatGroupSO katanaStat;
        public StatGroupSO cresentBladeStat;
        public StatGroupSO crossStat;


        private void InitDict()
        {
            StatGroup = new Dictionary<CharacterEnum, StatGroupSO>();

            katanaStat = ScriptableObject.Instantiate(katanaStat);
            for (int i = 0; i < katanaStat.statList.Count; i++) katanaStat.statList[i] = ScriptableObject.Instantiate(katanaStat.statList[i]);
            StatGroup.Add(CharacterEnum.Katana, katanaStat);

            cresentBladeStat = ScriptableObject.Instantiate(cresentBladeStat);
            for (int i = 0; i < cresentBladeStat.statList.Count; i++) cresentBladeStat.statList[i] = ScriptableObject.Instantiate(cresentBladeStat.statList[i]);
            StatGroup.Add(CharacterEnum.CrecentBlade, cresentBladeStat);

            crossStat = ScriptableObject.Instantiate(crossStat);
            for (int i = 0; i < crossStat.statList.Count; i++) crossStat.statList[i] = ScriptableObject.Instantiate(crossStat.statList[i]);
            StatGroup.Add(CharacterEnum.Cross, crossStat);
        }

        public bool TryGetStat(CharacterEnum character, StatusEnumType stat, out StatSO statSO)
            => StatGroup[character].TryGetStat(stat, out statSO);

        public bool TryAddModifier(CharacterEnum character, StatusEnumType stat, float value)
        {
            if (TryGetStat(character, stat, out StatSO statSO))
            {
                statSO.AddModifier(value);
                return true;
            }
            Debug.LogError($"You try to add value to not exsist stat [ Character:{character} ][ Stat:{stat}]");
            return false;
        }
    }


    [Serializable]
    public class StatSave
    {
        public List<TechTreeSave> treeSave = new List<TechTreeSave>();
    }

    [Serializable]
    public class TechTreeSave
    {
        public CharacterEnum characterType;
        public List<string> openListGUID;
    }
}
