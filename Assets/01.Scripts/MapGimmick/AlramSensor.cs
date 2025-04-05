using Agents.Players;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

namespace Ingame.Gimmick
{
    public class AlramSensor : MonoBehaviour
    {
        [SerializeField] private LayerMask _detectLayer;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private float _angle;
        [SerializeField] private float _distance;
        [SerializeField] private int _concentrate;
        [Space]
        [SerializeField] private Alram _alram;
        [SerializeField] private Light2D _light;

        private void Update()
        {
            Detect();
            UpdateLight();
        }

        private void UpdateLight()
        {
            Vector2 direction = (_targetTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _light.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            _light.pointLightOuterRadius = _distance;
            _light.pointLightInnerAngle = _angle * 2;
            _light.pointLightOuterAngle = _angle * 2;
        }

        private void Detect()
        {
            Vector2 originDir = (_targetTransform.position - transform.position).normalized;
            float currentAngle = -_angle;

            while (currentAngle <= _angle)
            {
                Quaternion rotate = Quaternion.Euler(0, 0, currentAngle);
                Vector2 direction = rotate * originDir;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _distance, _detectLayer);

                if (hit.collider != null)
                {
                    if (hit.collider.TryGetComponent(out Player player))
                    {
                        SpawnEnemy();
                        break;
                    }
                }

                currentAngle += _concentrate;
            }
        }

        private void SpawnEnemy()
        {
            _alram.StartRining();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_targetTransform == null || _concentrate <= 0.1f) return;

            Vector2 originDir = (_targetTransform.position - transform.position).normalized;
            float currentAngle = -_angle;

            while (currentAngle <= _angle)
            {
                Quaternion rotate = Quaternion.Euler(0, 0, currentAngle);
                Vector2 direction = rotate * originDir;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _distance, _detectLayer);

                if (hit.collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(transform.position, hit.point);
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * _distance);
                }

                currentAngle += _concentrate;
            }
        }
#endif

    }
}
