using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.Combat.SubWeaponSystem
{
    public class ThrowWeaponAmountPanel : WeaponDetailPanel
    {
        [SerializeField] private Image _weaponIconImage;
        [SerializeField] private TextMeshProUGUI _amountText;

        public override void HandleWeaponCountChange(int currentCount, int maxCount)
        {
            _amountText.text = currentCount.ToString();
        }

        public override void SetData(SubWeaponSO data)
        {
            _weaponIconImage.sprite = data.subWeaponSprite;
        }
    }
}