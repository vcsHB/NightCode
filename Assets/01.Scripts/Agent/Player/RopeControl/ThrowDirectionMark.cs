using UnityEngine;
namespace Agents.Players
{

    public class ThrowDirectionMark : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void SetTargetMark(bool value)
        {
            _spriteRenderer.enabled = value;

        }
        public void SetDirection(Vector2 direction)
        {
            transform.right = direction;
        }
    }
}