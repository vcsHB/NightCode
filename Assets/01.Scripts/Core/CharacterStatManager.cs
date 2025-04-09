using System.Collections.Generic;
using UnityEngine;


namespace StatSystem
{
    public class CharacterStatManager : MonoSingleton<CharacterStatManager>
    {
        public Dictionary<CharacterEnum, StatGroupSO> statGroup;
        public StatGroupSO katanaStat;
        public StatGroupSO cresentBladeStat;
        public StatGroupSO crossStat;

        protected override void Awake()
        {
            base.Awake();
            statGroup = new Dictionary<CharacterEnum, StatGroupSO>();

            katanaStat = ScriptableObject.Instantiate(katanaStat);
            katanaStat.statList.ForEach(stat => stat = ScriptableObject.Instantiate(stat));
            statGroup.Add(CharacterEnum.Katana, katanaStat);

            cresentBladeStat = ScriptableObject.Instantiate(cresentBladeStat);
            cresentBladeStat.statList.ForEach(stat => stat = ScriptableObject.Instantiate(stat));
            statGroup.Add(CharacterEnum.CrecentBlade, cresentBladeStat);

            crossStat = ScriptableObject.Instantiate(crossStat);
            crossStat.statList.ForEach(stat => stat = ScriptableObject.Instantiate(stat));
            statGroup.Add(CharacterEnum.Cross, crossStat);
        }

        public bool TryGetStat(CharacterEnum character, StatusEnumType stat, out StatSO statSO)
            => statGroup[character].TryGetStat(stat, out statSO);
    }
}
