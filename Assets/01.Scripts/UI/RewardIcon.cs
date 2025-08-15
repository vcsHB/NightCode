using Chipset;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _chipsetText;

    public void SetRewardChipset(ChipsetSO chipset)
    {
        _icon.sprite = chipset.icon;
        _chipsetText.text = chipset.chipsetName;
    }
}
