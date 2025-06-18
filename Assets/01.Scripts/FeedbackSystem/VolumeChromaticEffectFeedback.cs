using System.Collections;
using Core;
using Core.VolumeControlSystem;
using UnityEngine;
namespace FeedbackSystem
{

    public class VolumeChromaticEffectFeedback : Feedback
    {
        [SerializeField] private float _level = 0.1f;
        [SerializeField] private float duration = 0.1f;
        private ChromaticAberrationController _controller;
        private void Start()
        {
            _controller = VolumeManager.Instance.GetCompo<ChromaticAberrationController>();
        }

        public override void CreateFeedback()
        {
            if(!gameObject.activeInHierarchy) return;
            if(_controller == null) return;
            StartCoroutine(EffectCoroutine());
        }
        private IEnumerator EffectCoroutine()
        {
            _controller.SetChromatic(_level);
            yield return new WaitForSeconds(duration);
            _controller.HandleDisableChromatic();
        }

        public override void FinishFeedback()
        {
            _controller.HandleDisableChromatic();
        }
    }
}