using DG.Tweening;
using StatSystem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Office.CharacterSkillTree
{
    public class CharacterStatIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _characterName;

        [SerializeField] private RectTransform _indicatorBG;
        [SerializeField] private RectTransform _slotParent;
        [SerializeField] private StatSlot _statSlotPf;

        [SerializeField] private List<string> _characterNameList;

        public void SetCharacter(SkillTree tree)
        {
            _slotParent.GetComponentsInChildren<StatSlot>().ToList()
                .ForEach(prevSlot => Destroy(prevSlot.gameObject));

            _characterName.SetText(_characterNameList[(int)tree.characterType]);
            StatGroupSO statGroup = CharacterStatManager.Instance.StatGroup[tree.characterType];

            statGroup.statList.ForEach(stat =>
            {
                StatSlot slot = Instantiate(_statSlotPf, _slotParent);
                slot.SetStat(stat);
            });



            //SaveManager.Instance.GetStatValue(tree.characterType).openListGUID.ForEach(guid =>
            //{
            //    statGroup.statList.ForEach(stat =>
            //    {
            //        //StatSO statInstance = ScriptableObject.Instantiate(stat);
            //        //tree.treeSO.nodes.ForEach(node =>
            //        //{
            //        //    if (node is StatIncNodeSO increaseNode)
            //        //    {
            //        //        for (int i = 0; i < increaseNode.stat.Length; i++)
            //        //        {

            //        //            if (node.guid == guid) statInstance.AddModifier()
            //        //        }
            //        //    }
            //        //});

            //        StatSlot slot = Instantiate(_statSlotPf, _slotParent);
            //        slot.SetStat(stat);
            //    });
            //});
        }
    }
}
