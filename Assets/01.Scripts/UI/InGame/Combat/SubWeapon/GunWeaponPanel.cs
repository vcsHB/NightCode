using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.Combat.SubWeaponSystem
{
    public class GunWeaponPanel : WeaponDetailPanel
    {
        [SerializeField] private Image _leftBulletImage;
        [SerializeField] private TextMeshProUGUI _amountText;            

        public override void SetData(SubWeaponSO data)
        {

        }


    }
}