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
        [SerializeField] private Image _intervalImage;
        private Material _intervalMaterial;
        private readonly int _intervalPropertyHash = Shader.PropertyToID("_BulletAmount");


        protected override void Awake()
        {
            base.Awake();
            _intervalMaterial = _intervalImage.material;    
        }
        public override void HandleWeaponCountChange(int currentCount, int maxCount)
        {
            _amountText.text = $"{currentCount}/{maxCount}";
            _leftBulletImage.fillAmount = (float)currentCount / maxCount;
            _intervalMaterial.SetFloat(_intervalPropertyHash, maxCount);

        }

        public override void SetData(SubWeaponSO data)
        {
        }


    }
}