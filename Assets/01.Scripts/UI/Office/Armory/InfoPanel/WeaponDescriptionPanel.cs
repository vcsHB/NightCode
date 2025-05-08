using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public class WeaponDescriptionPanel : WeaponInformationPanel
    {
        [SerializeField] private TextMeshProUGUI _weaponDescriptionText;

        public override void SetWeaponData(SubWeaponSO weapon, SubWeaponData weaponData)
        {
            _weaponDescriptionText.text = weapon.description;
        }
    }
}