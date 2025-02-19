using System.Collections;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage
{

    public class BloodVFXPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform _rotationHandleTrm;
        [SerializeField] private SpriteRenderer _visualRenderer;

        private Transform _visualTrm;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _dissolveDuration;
        [SerializeField] private float _randomizeMin, _randomizeMax;
        [SerializeField] private Sprite[] _bloodSprites;
        private Material _bloodMaterial;
        private int _dissolveHash = Shader.PropertyToID("_DissolveLevel");

        [field:SerializeField] public PoolingType type { get; set; }
        public GameObject ObjectPrefab => gameObject;

        private void Awake()
        {
            _bloodMaterial = _visualRenderer.material;
            _visualTrm = _visualRenderer.transform;
        }

        public void Play(Vector2 direction)
        {
            _visualRenderer.sprite = _bloodSprites[Random.Range(0, _bloodSprites.Length)];
            SetDissolveLevel(1f);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rotationHandleTrm.rotation = Quaternion.Euler(0, 0, angle + Random.Range(_randomizeMin, _randomizeMax));
            StartCoroutine(EffectCoroutine());
        }

        private IEnumerator EffectCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime - _dissolveDuration);
            float currentTime = 0f;
            while (currentTime > _dissolveDuration)
            {
                float ratio = currentTime / _dissolveDuration;
                SetDissolveLevel(ratio);
                currentTime += Time.deltaTime;
                yield return null;
            }
            SetDissolveLevel(0f);
            PoolManager.Instance.Push(this);
        }

        private void SetDissolveLevel(float level)
        {
            _bloodMaterial.SetFloat(_dissolveHash, level);
        }

        public void ResetItem()
        {

        }
    }

}