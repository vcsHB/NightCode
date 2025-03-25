using UnityEngine;

namespace Cafe
{
    public class CafeEntityRenderer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetAnimParam(int hash, bool value)
        {
            _animator.SetBool(hash, value);
        }

        public void Flip()
        {
            transform.localScale
                = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
    }
}
