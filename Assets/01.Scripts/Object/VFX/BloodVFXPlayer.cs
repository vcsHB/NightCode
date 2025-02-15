using System.Collections;
using UnityEngine;

namespace ObjectManage
{

    public class BloodVFXPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _rotationHandleTrm;
        [SerializeField] private SpriteRenderer _visualRenderer;

        private Transform _visualTrm;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _dissolveDuration;
        [SerializeField] private float _randomizeMin, _randomizeMax;
        [SerializeField] private Color _bloodColor;
        private Material _bloodMaterial;
        private int _dissolveHash = Shader.PropertyToID("DissolveLevel");

        private void Awake()
        {
            _bloodMaterial = _visualRenderer.material;
            _visualTrm = _visualRenderer.transform;
        }

        public void Play(Vector2 direction)
        {
            _visualRenderer.color = _bloodColor;
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
        }

        private void SetDissolveLevel(float level)
        {
            _bloodMaterial.SetFloat(_dissolveHash, level);
        }
    }

}