using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
namespace FeedbackSystem
{

    public class BlinkFeedback : Feedback
    {
        [SerializeField] private SpriteRenderer _targetRenderer;
        [SerializeField, Range(0f, 1f)] private float _blinkLevel = 0.4f;
        [SerializeField] private float _blinkTime = 0.2f;
        private readonly int _blinkValueHash = Shader.PropertyToID("_BlinkValue");
        private Material _targetMaterial;
        private Coroutine _coroutine = null;
        private float _defaultBlink;

        protected override void Awake()
        {
            base.Awake();
            _targetMaterial = _targetRenderer.material;
            _defaultBlink = _targetMaterial.GetFloat(_blinkValueHash);
        }

        private void OnDisable()
        {

        }

        public override void CreateFeedback()
        {
            _coroutine = StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            _targetMaterial.SetFloat(_blinkValueHash, _blinkLevel);
            yield return new WaitForSeconds(_blinkTime);
            _targetMaterial.SetFloat(_blinkValueHash, _defaultBlink);
            _coroutine = null;
        }

        public override void FinishFeedback()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _targetMaterial.SetFloat(_blinkValueHash, _defaultBlink);
        }
    }
}