using System.Collections;
using DG.Tweening;
using ObjectPooling;
using UnityEngine;

namespace ObjectManage.VFX
{

    public class KatanaSlashVFXPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private Transform _trailTrm;
        private TrailRenderer _trailRenderer;
        [SerializeField] private float _slashSize;
        [SerializeField] private float _slashDuration = 0.2f;
        [SerializeField] private float _destroyDelay = 0.2f;
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        private void Awake()
        {
            _trailRenderer = _trailTrm.GetComponent<TrailRenderer>();
            _trailRenderer.Clear();
        }

        public void Slash(Vector2 centerPos, Vector2 direction)
        {
            _trailRenderer.enabled = false;
            direction.Normalize();
            transform.position = centerPos;
            Vector2 movement = direction * _slashSize;
            _trailTrm.localPosition = -movement;
            _trailRenderer.enabled = true;
            _trailTrm.DOLocalMove(movement, _slashDuration).OnComplete(() => StartCoroutine(SlashOverCoroutine()));
            
        }

        private IEnumerator SlashOverCoroutine()
        {
            yield return new WaitForSeconds(_slashDuration);
            PoolManager.Instance.Push(this);
        }
        public void ResetItem()
        {
            _trailRenderer.Clear();

        }
    }

}