using Combat.SubWeaponSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.OfficeScene.Armory
{

    public class WeaponStatusSlot : MonoBehaviour
    {
        [SerializeField] private Image _statusImage;
        [SerializeField] private TextMeshProUGUI _statusDetailText;
        [SerializeField] private WeaponStatusSlotMaxPanel _maxPanel;
        [SerializeField] private WeaponStatusSlotValuePanel _valuePanel;


        public void SetStatusIncrease(float currentValue, float nextValue, WeaponStatusSO statusSO)
        {
            SetStatusInfo(statusSO);
            _valuePanel.Open();
            _maxPanel.Close();
            _valuePanel.SetValue(currentValue, nextValue);

        }

        public void SetMaxStatus(float currentValue, WeaponStatusSO statusSO)
        {
            SetStatusInfo(statusSO);
            _valuePanel.Close();
            _maxPanel.SetValue(currentValue);
            _maxPanel.Open();

        }

        private void SetStatusInfo(WeaponStatusSO statusSO)
        {
            _statusImage.sprite = statusSO.statusIcon;
            _statusDetailText.text = statusSO.description;
        }



        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

    }
}