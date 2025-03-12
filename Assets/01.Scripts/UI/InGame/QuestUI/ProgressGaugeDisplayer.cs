using DG.Tweening;
using QuestSystem;
using UnityEngine;
using UnityEngine.UI;
namespace UI.InGame.GameUI.QuestSyetem
{

    public class ProgressGaugeDisplayer : ProgressDisplayer
    {
        [SerializeField] private Slider _progressGaugeSlider;
        [SerializeField] private float _tweenDuration;
        public override void SetProgress(QuestData data)
        {
            base.SetProgress(data);
            _progressGaugeSlider.value = 0f;
            _progressGaugeSlider.DOValue(data.ProgressRatio, _tweenDuration).SetUpdate(true);
        }

    }
}