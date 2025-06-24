using System;
using Agents.Players.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.NodeViewScene.WeaponSelectionUIs
{
    public class WeaponInfoPanel : MonoBehaviour
    {
        public event Action<PlayerWeaponSO> OnWeaponSelectEvent;
        [SerializeField] private WeaponSelectionPanel _weaponSelectionPanel;
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private TextMeshProUGUI _weaponIdText;
        [SerializeField] private TextMeshProUGUI _weaponDescriptionText;

        private const string weaponIdTextFormat = "WEAPON ID: ";

        private void Awake()
        {
            _weaponSelectionPanel.OnWeaponSelectionEvent += HandleWeaponSelectEvent;
        }

        private void OnDestroy()
        {
            _weaponSelectionPanel.OnWeaponSelectionEvent -= HandleWeaponSelectEvent;

        }

        private void HandleWeaponSelectEvent(PlayerWeaponSO data)
        {
            SetWeaponData(data);
            OnWeaponSelectEvent?.Invoke(data);
        }

        public void SetWeaponData(PlayerWeaponSO weaponSO)
        {
            _weaponIcon.sprite = weaponSO.weaponIcon;
            _weaponNameText.text = weaponSO.weaponName;
            _weaponIdText.text = $"{weaponIdTextFormat}{weaponSO.id}";
            _weaponDescriptionText.text = weaponSO.weaponDescription;
        }


        public void InitializeData(WeaponDataGroup dataGroup)
        {
            _weaponSelectionPanel.InitializeData(dataGroup);
        }

        public void SelectCharacter(CharacterEnum character)
        {
            _weaponSelectionPanel.SelectCharacter(character);
        }
    }
}