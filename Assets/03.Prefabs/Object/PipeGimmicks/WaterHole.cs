using System.Collections;
using UnityEngine;

namespace ObjectManage.GimmickObjects
{

    public class WaterHole : MonoBehaviour
    {
        [SerializeField] private LineRenderer _waterRenderer;
        [SerializeField] private LayerMask _collisionLayer;
        [SerializeField] private ParticleSystem _waterSurfaceVFX;
        [SerializeField] private ParticleSystem _waterHoleVFX;
        [SerializeField] private float _waterFallSpeed;
        [SerializeField] private float _waterFallMaxDuration;
        private Vector2 _waterSurfacePosition;
        [SerializeField] private bool _isActive;
        [SerializeField] private float _fillWaterAmount = 1f;
        private IWaterFillable _water;

        private void Start()
        {
            SetWaterFall(_isActive);
        }

        private void DetectWater()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, _collisionLayer);
            if (hit.collider != null)
            {
                _waterSurfacePosition = hit.point;
                if (_water == null)
                {
                    hit.collider.TryGetComponent(out _water);
                }
            }
        }

        private void Update()
        {
            if (!_isActive) return;
            DetectWater();
            if (_water != null)
                _water.Fill(_fillWaterAmount * Time.deltaTime);

            _waterRenderer.SetPosition(1, _waterSurfacePosition);
            _waterSurfaceVFX.transform.position = _waterSurfacePosition;
        }

        [ContextMenu("DebugWaterEnable")]
        private void SetWaterEnable()
        {
            SetWaterFall(true);
        }
        [ContextMenu("DebugWaterDisable")]
        private void SetWaterDsiable()
        {
            SetWaterFall(false);
        }


        public void SetWaterFall(bool value)
        {
            if (value)
            {
                StartCoroutine(SetWaterEnableCoroutine());
            }
            else
            {
                StartCoroutine(SetWaterDisableCoroutine());

            }

        }
        private IEnumerator SetWaterEnableCoroutine()
        {
            DetectWater();
            Vector2 startPos = transform.position;
            _waterRenderer.SetPosition(0, startPos);
            _waterRenderer.enabled = true;
            _waterHoleVFX.Play();
            float currentTime = 0f;
            while (currentTime < _waterFallMaxDuration)
            {
                currentTime += Time.deltaTime * _waterFallSpeed;
                float ratio = currentTime / _waterFallMaxDuration;

                _waterRenderer.SetPosition(1, Vector2.Lerp(startPos, _waterSurfacePosition, ratio));
                yield return null;

            }
            _isActive = true;
            _waterSurfaceVFX.Play();
        }

        private IEnumerator SetWaterDisableCoroutine()
        {
            DetectWater();
            Vector2 startPos = transform.position;
            _isActive = false;
            _waterHoleVFX.Stop();
            float currentTime = 0f;
            while (currentTime < _waterFallMaxDuration)
            {
                currentTime += Time.deltaTime * _waterFallSpeed;
                float ratio = currentTime / _waterFallMaxDuration;

                _waterRenderer.SetPosition(0, Vector2.Lerp(startPos, _waterSurfacePosition, ratio));
                yield return null;

            }
            _waterRenderer.enabled = false;
            _waterSurfaceVFX.Stop();
        }
    }
}
