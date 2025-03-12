using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.QuestSyetem
{

    public class ProgressGaugeDisplayer : ProgressDisplayer
    {
        [SerializeField] private Slider _progressGaugeSlider;
        [SerializeField] private float _tweenDuration;
        public override void SetProgress(float ratio)
        {
            base.SetProgress(ratio);
            _progressGaugeSlider.value = 0f;
            _progressGaugeSlider.DOValue(ratio, _tweenDuration);
        }

    }
}