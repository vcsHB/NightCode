using System.Collections;
using UnityEngine;
namespace FeedbackSystem
{

    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _targetRenderer;
        [SerializeField] private float _blinkTime = 0.2f;
        private readonly int _blinkValueHash = Shader.PropertyToID("_BlinkValue");
        private float beforeValue;
        private Material _targetMaterial;
        private Coroutine _coroutine = null;

        protected override void Awake()
        {
            base.Awake();
            _targetMaterial = _targetRenderer.material;
        }

        public override void CreateFeedback()
        {
            _coroutine = StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            beforeValue = _targetMaterial.GetFloat(_blinkValueHash);
            _targetMaterial.SetFloat(_blinkValueHash, 0.4f);
            yield return new WaitForSeconds(_blinkTime);
            _targetMaterial.SetFloat(_blinkValueHash, beforeValue);

        }

        public override void FinishFeedback()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _targetMaterial.SetFloat(_blinkValueHash, 0);
        }
    }
}