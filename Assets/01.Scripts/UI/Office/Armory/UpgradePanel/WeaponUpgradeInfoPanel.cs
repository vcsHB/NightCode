using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public class WeaponUpgradeInfoPanel : WeaponInformationPanel
    {
        [SerializeField] private TextMeshProUGUI _costAmountText;
        [SerializeField] private TextMeshProUGUI _previousLevelText;
        [SerializeField] private TextMeshProUGUI _nextLevelText;
        public override void SetWeaponData(SubWeaponSO weapon, SubWeaponData weaponData)
        {
            _costAmountText.text = weapon.levelUpData[weaponData.level - 1].cost.ToString();
            SetLevelText(weaponData.level, weapon.levelUpData.Length);
        }

        private void SetLevelText(int current, int max)
        {
            _previousLevelText.text = current.ToString();
            if (max > current)
            { // level5 : index4   5
                if (max >= current + 1)
                    _nextLevelText.text = $"Lv.{(current + 1).ToString()}(MAX)";
                else
                    _nextLevelText.text = $"Lv.{(current + 1).ToString()}";
            }
            else
                _nextLevelText.text = "NO DATA";

        }
    }
}