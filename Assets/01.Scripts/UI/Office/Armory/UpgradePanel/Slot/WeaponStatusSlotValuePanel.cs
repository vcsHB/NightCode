using TMPro;
using UnityEngine;
namespace UI.OfficeScene.Armory
{

    public class WeaponStatusSlotValuePanel : MonoBehaviour, IWindowPanel
    {
        [SerializeField] private TextMeshProUGUI _prevValueText;
        [SerializeField] private TextMeshProUGUI _nextValueText;

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);

        }

        public void SetValue(float prevValue, float nextValue)
        {
            _prevValueText.text = prevValue.ToString();
            _nextValueText.text = nextValue.ToString();
        }
    }
}