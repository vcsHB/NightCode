using UnityEngine;

namespace Dialog
{
    public class Emotion : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetIcon(Sprite icon, Vector2 offset)
        {
            if(_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            gameObject.SetActive(true);
            transform.localPosition = offset;
            _spriteRenderer.sprite = icon;
        }

        public void DisableEmotion()
        {
            gameObject.SetActive(false);
        }
    }
}
