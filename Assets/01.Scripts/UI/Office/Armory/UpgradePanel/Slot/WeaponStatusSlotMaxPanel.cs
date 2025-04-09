using TMPro;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public class WeaponStatusSlotMaxPanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private TextMeshProUGUI _valueText;
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);

        }
        public void SetValue(float value)
        {
            _valueText.text = $"{value} (MAX)";
        }
    }
}