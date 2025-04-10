using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.OfficeScene.Armory
{
    public class WeaponStatusPanel : WeaponInformationPanel
    {
        [Header("Essential Setting")]
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private Image _weaponLevelPreviewImage;
        [SerializeField] private Image _weaponImage;
        [SerializeField] private Sprite[] _previewPanelSprites;


        public override void SetWeaponData(SubWeaponSO weapon, SubWeaponData weaponData)
        {
            _weaponLevelPreviewImage.sprite = _previewPanelSprites[weaponData.level-1];
            _weaponImage.sprite = weapon.subWeaponSprite;
            _weaponNameText.text = $"{weapon.weaponName}";
        }

    }
}