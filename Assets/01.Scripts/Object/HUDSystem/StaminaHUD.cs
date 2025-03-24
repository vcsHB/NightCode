using UnityEngine;
using UnityEngine.UI;
namespace HUDSystem
{

    public class StaminaHUD : HUDObject
    {
        [SerializeField] private Image _fillImage;

        public void HandleRefresh(int current, int max)
        {
            HandleRefresh((float)current / max);
        }
        public void HandleRefresh(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            _fillImage.fillAmount = ratio;
        }
    }
}