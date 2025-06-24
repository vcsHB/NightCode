using UnityEngine;
namespace Shop
{

    public class GoodsPreviewObject : MonoBehaviour
    {
        private SpriteRenderer _previewRenderer;
        [SerializeField] private Sprite _soldOutIcon;

        private void Awake()
        {

            _previewRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetSoldOut()
        {
            _previewRenderer.sprite = _soldOutIcon;
        }

        public void SetSprite(Sprite newSprite)
        {
            _previewRenderer.sprite = newSprite;
        }
    }
}