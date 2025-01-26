using System.Collections;
using ObjectPooling;
using Unity.XR.OpenVR;
using UnityEngine;

namespace ObjectManage
{

    public class AfterShadowVFXPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private float _lifeTime = 1f;
        [SerializeField] private Gradient _colorGradient;
        private SpriteRenderer _spriteRenderer;


        private void Awake()
        {
            _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
        }

        public void Initialize(Vector2 position, float lifeTime, Gradient gradient, Sprite sprite, bool isFlip = false)
        {
            transform.position = position;
            _lifeTime = lifeTime;
            _colorGradient = gradient;
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.color = _colorGradient.Evaluate(0f);
            _spriteRenderer.flipX = isFlip;
        }

        public void Play()
        {
            StartCoroutine(PlayEffectCoroutine());
        }

        private IEnumerator PlayEffectCoroutine()
        {
            float currentTime = 0f;
            while(currentTime < _lifeTime)
            {
                currentTime += Time.deltaTime;
                float ratio = currentTime / _lifeTime;
                _spriteRenderer.color = _colorGradient.Evaluate(ratio);
                yield return null;
            }
            PoolManager.Instance.Push(this);
        }

        public void ResetItem()
        {

        }
    }

}