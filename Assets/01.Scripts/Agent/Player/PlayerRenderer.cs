using System;
using System.Collections;
using Agents.Animate;
using UnityEngine;

namespace Agents.Players
{

    public class PlayerRenderer : AgentRenderer
    {
        [field: SerializeField] public AnimParamSO IdleParam { get; private set; }
        [field: SerializeField] public AnimParamSO MoveParam { get; private set; }
        [field: SerializeField] public AnimParamSO FallParam { get; private set; }
        [field: SerializeField] public AnimParamSO JumpParam { get; private set; }
        [field: SerializeField] public AnimParamSO SwingParam { get; private set; }
        [field: SerializeField] public AnimParamSO HangParam { get; private set; }
        [field: SerializeField] public AnimParamSO HoldWallParam { get; private set; }
        [field: SerializeField] public AnimParamSO SlideDownParam { get; private set; }
        [field: SerializeField] public AnimParamSO ClimbParam { get; private set; }
        [field: SerializeField] public AnimParamSO EnterParam { get; private set; }
        [field: SerializeField] public AnimParamSO ExitParam { get; private set; }


        [field: SerializeField] public AnimParamSO AttackParam { get; private set; }
        [field: SerializeField] public AnimParamSO GrabParam { get; private set; }
        [field: SerializeField] public AnimParamSO PullParam { get; private set; }
        [field: SerializeField] public AnimParamSO AirAttackParam { get; private set; }
        [field: SerializeField] public AnimParamSO SkillParam { get; private set; }

        protected bool _isLockRotation = true;
        private readonly int _dissolveHash = Shader.PropertyToID("_Dissolve");
        private float _dissolveDuration = 0.2f;


        protected override void Awake()
        {
            base.Awake();
        }
        public void SetLockRotation(bool value)
        {
            _isLockRotation = value;
            if (value)
                transform.localRotation = Quaternion.identity;
        }

        public void SetRotate(Vector2 upDirection)
        {
            if (_isLockRotation) return;

            bool isFlipped = Mathf.Approximately(Mathf.Abs(_agent.transform.eulerAngles.y), 180f);
            float angle = Mathf.Atan2(upDirection.y, upDirection.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0, isFlipped ? 180f : 0f, isFlipped ? -angle : angle);
        }

        public void SetDissolve(bool value, Action onComplete = null)
        {
            StartCoroutine(DissolveCoroutine(value, onComplete));
        }

        private IEnumerator DissolveCoroutine(bool value, Action onComplete = null)
        {
            float currentTime = 0f;
            while (currentTime < _dissolveDuration)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _dissolveDuration;
                _spriteRenderer.material.SetFloat(_dissolveHash, value ? ratio : 1 - ratio);
                yield return null;
            }
            if (onComplete != null)
                onComplete();
        }

    }
}