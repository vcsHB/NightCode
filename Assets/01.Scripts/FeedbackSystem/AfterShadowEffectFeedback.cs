using System.Collections;
using Agents;
using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace FeedbackSystem
{
    public class AfterShadowEffectFeedback : Feedback
    {
        [SerializeField] private AgentRenderer _agentRenderer;
        [SerializeField] private SpriteRenderer _ownerRenderer;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _generateTerm = 0.4f;
        [SerializeField] private float _shadowLifeTime = 0.1f;
        private Coroutine _effectCoroutine;

        public override void CreateFeedback()
        {
            _effectCoroutine = StartCoroutine(PlayEffectCoroutine());
        }

        private IEnumerator PlayEffectCoroutine()
        {
            float currentTime = 0f;
            float currentGenerateTime = 0f;
            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                currentGenerateTime += Time.deltaTime;
                float ratio = currentTime / _duration;
                if (currentGenerateTime > _generateTerm)
                {
                    currentGenerateTime = 0f;
                    AfterShadowVFXPlayer effect = PoolManager.Instance.Pop(PoolingType.AfterShadowEffect) as AfterShadowVFXPlayer;
                    effect.Initialize(
                        transform.position,
                        _shadowLifeTime,
                        _colorGradient,
                        _ownerRenderer.sprite,
                        _agentRenderer.FacingDirection < 0f
                    );
                    effect.Play();

                }
                yield return null;
            }
            _effectCoroutine = null;

        }

        public override void FinishFeedback()
        {
            if (_effectCoroutine == null) return;
            StopCoroutine(_effectCoroutine);
            _effectCoroutine = null;
        }
    }
}