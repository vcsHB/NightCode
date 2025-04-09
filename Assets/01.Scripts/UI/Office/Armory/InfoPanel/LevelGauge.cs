using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.OfficeScene
{

    public class LevelGauge : MonoBehaviour
    {

        [SerializeField] private Slider _levelGauge;
        [SerializeField]private float _gaugeFillDuration = 0.2f;
        [SerializeField] private TextMeshProUGUI _levelText;


        public void SetLevel(int current, int max)
        {
            float ratio = current / (float)max;

            _levelText.text = $"Lv.{current}";
            _levelGauge.value = 0f;
            _levelGauge.DOValue(ratio, _gaugeFillDuration);

        }
    }
}