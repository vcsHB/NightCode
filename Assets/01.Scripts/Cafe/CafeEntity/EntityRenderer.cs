using UnityEngine;

namespace Base.Entity
{
    public class EntityRenderer : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private float _moveDirection = 1;

        public float MoveDirection => _moveDirection;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        public void SetAnimParam(int hash, bool value)
        {
            if (_animator == null) _animator = GetComponent<Animator>();
            _animator.SetBool(hash, value);
        }

        public void Flip()
        {
            transform.localScale
                = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }

        public void FlipControl(float direction)
        {
            if (direction == 0) return;

            if (_moveDirection != direction) Flip();
            _moveDirection = direction;
        }
    }
}
