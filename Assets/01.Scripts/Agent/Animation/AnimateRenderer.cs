using UnityEngine;
namespace Agents.Animate
{

    public class AnimateRenderer : MonoBehaviour
    {
        protected Animator _animator;
        protected SpriteRenderer _spriteRenderer;
        public Animator Animator => _animator;

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);
    }
}