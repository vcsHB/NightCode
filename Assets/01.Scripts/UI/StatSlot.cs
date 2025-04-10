using StatSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Office.CharacterSkillTree
{
    public class StatSlot : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _pointText;

        public void SetStat(StatSO stat)
        {
            _icon.sprite = stat.Icon;
            _nameText.SetText(stat.displayName);
            _pointText.SetText($"   {stat.BaseValue}");
        }
    }
}
