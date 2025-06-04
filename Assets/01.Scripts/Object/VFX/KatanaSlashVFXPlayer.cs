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
        [SerializeField] private float _followSmoothTime = 0.05f;
        public GameObject ObjectPrefab => gameObject;
        private Coroutine _followCoroutine;
        private Vector2 _velocity; // SmoothDamp

        private void Awake()
        {
            _trailRenderer = _trailTrm.GetComponent<TrailRenderer>();
            _trailRenderer.Clear();
        }

        public void SetGradient(Gradient gradient)
        {
            _trailRenderer.colorGradient = gradient;
        }

        public void Slash(Vector2 centerPos, Vector2 direction, float slashSize = 0f)
        {
            _trailRenderer.enabled = false;
            direction.Normalize();
            transform.position = centerPos;
            Vector2 movement = direction * (Mathf.Approximately(slashSize, 0f) ? _slashSize : slashSize);
            _trailTrm.localPosition = -movement;
            _trailRenderer.enabled = true;
            _trailTrm.DOLocalMove(movement, _slashDuration).OnComplete(() => StartCoroutine(SlashOverCoroutine()));

        }
        public void SlashLerp(Vector2 startPos, Vector2 endPos, float speedMultiplier = 1)
        {
            _trailRenderer.enabled = false;
            transform.position = startPos;
            _trailTrm.position = startPos;
            _trailRenderer.enabled = true;
            _trailTrm.DOMove(endPos, _slashDuration * speedMultiplier).OnComplete(() => StartCoroutine(SlashOverCoroutine()));

        }


        public void SlashLerpFollow(Vector2 startPos, Transform targetTransform, float speedMultiplier = 1f)
        {
            if (_followCoroutine != null)
                StopCoroutine(_followCoroutine);

            _velocity = Vector2.zero;

            _trailRenderer.enabled = false;
            transform.position = startPos;
            _trailTrm.position = startPos;
            _trailRenderer.enabled = true;

            _followCoroutine = StartCoroutine(SmoothFollowCoroutine(targetTransform, speedMultiplier));
        }

        private IEnumerator SmoothFollowCoroutine(Transform targetTransform, float speedMultiplier)
        {
            float duration = _slashDuration * speedMultiplier;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (targetTransform == null)
                    yield break;

                elapsed += Time.deltaTime;

                _trailTrm.position = Vector2.SmoothDamp(
                    _trailTrm.position,
                    targetTransform.position,
                    ref _velocity,
                    _followSmoothTime
                );

                yield return null;
            }

            _trailTrm.position = targetTransform.position;
            StartCoroutine(SlashOverCoroutine());
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