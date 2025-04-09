using Combat.SubWeaponSystem;
using UnityEngine;

namespace UI.OfficeScene.Armory
{

    public class WeaponInfoPanel : UIPanel
    {
        [SerializeField] private float _activeXPos;
        [SerializeField] private float _disableXPos;
        [SerializeField] private float _moveDuration = 0.2f;

        [SerializeField] private RectTransform _panelTrm;
        private WeaponInformationPanel[] _weaponInformationPanels;

        protected override void Awake()
        {
            base.Awake();
            _weaponInformationPanels = GetComponentsInChildren<WeaponInformationPanel>();

        }

        public override void Open()
        {
            base.Open();

        }

        public void SetWeaponData(SubWeaponSO weaponSO, SubWeaponData data)
        {
            for (int i = 0; i < _weaponInformationPanels.Length; i++)
            {
                _weaponInformationPanels[i].SetWeaponData(weaponSO, data);
            }
        }
    }

}