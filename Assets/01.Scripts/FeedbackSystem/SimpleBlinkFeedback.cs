using System.Collections;
using UnityEngine;
namespace FeedbackSystem
{

    public class SimpleBlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _targetRenderer;
        [SerializeField] private float _blinkTime = 0.2f;
        [SerializeField] private Material _targetMaterial;
        private Material _defaultMaterial;
        private Coroutine _coroutine = null;

        protected override void Awake()
        {
            base.Awake();

            _defaultMaterial = _targetRenderer.material;
        }

        public override void CreateFeedback()
        {
            _coroutine = StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            _targetRenderer.material = _targetMaterial;
            yield return new WaitForSeconds(_blinkTime);
            _targetRenderer.material = _defaultMaterial;
        }

        public override void FinishFeedback()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _targetRenderer.material = _defaultMaterial;
        }
    }
}