using Agents.Players.WeaponSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.CharacterSelector
{

    public class CharacterWeaponInfoPanel : UIPanel
    {
        [SerializeField] private Image _cooldownGaugeImage;
        [SerializeField] private Image _skillIconImage;
        [SerializeField] private Image _weaponImage;

        public void Initialize(PlayerWeaponSO weaponData)
        {
            _weaponImage.sprite = weaponData.weaponIcon;
            _cooldownGaugeImage.color = weaponData.weaponColor;
            _skillIconImage.sprite = weaponData.skillSO.skillIcon;
        }

        public void HandleRefreshCooltime(float current, float max)
        {
            float ratio = current / max;
            _cooldownGaugeImage.fillAmount = ratio;

        }
    }
}