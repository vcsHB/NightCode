using UnityEngine;

namespace Basement.NPC
{
    public class NPCRenderer : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

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
