using System;
using Agents.Players.WeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.NodeViewScene.WeaponSelectionUIs
{

    public class WeaponSelectionSlot : MonoBehaviour
    {
        public Action<PlayerWeaponSO> OnWeaponSelectEvent;
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private Image _weaponImage;
        [SerializeField] private CanvasGroup _selectionPanel;
        [SerializeField] private CanvasGroup _otherCharacterSelectionPanel;
        private PlayerWeaponSO _data;
        private Button _button;
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HandleSelectWeapon);
        }

        public void Initialize(PlayerWeaponSO weaponData)// Data of weapon is using by other Character
        {
            _data = weaponData;
            _weaponImage.sprite = weaponData.weaponIcon;
            _weaponImage.SetNativeSize();
            _weaponNameText.text = _data.weaponName;
        }

        public void SetSelectionPanel()
        {
            _selectionPanel.alpha = 1;
            _selectionPanel.blocksRaycasts = true;

            _otherCharacterSelectionPanel.alpha = 0;
            _otherCharacterSelectionPanel.blocksRaycasts = false;
        }

        public void SetOtherCharacterSelectionPanel()
        {
            _selectionPanel.alpha = 0;
            _selectionPanel.blocksRaycasts = false;

            _otherCharacterSelectionPanel.alpha = 1;
            _otherCharacterSelectionPanel.blocksRaycasts = true;
        }

        public void UnSetSelectionPanel()
        {
            _selectionPanel.alpha = 0;
            _selectionPanel.blocksRaycasts = false;

            _otherCharacterSelectionPanel.alpha = 0;
            _otherCharacterSelectionPanel.blocksRaycasts = false;
        }


        private void HandleSelectWeapon()
        {
            OnWeaponSelectEvent?.Invoke(_data);
        }
    }
}