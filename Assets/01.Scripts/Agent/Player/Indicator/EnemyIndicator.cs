using System;
using UnityEngine;

namespace Agents.Players
{
    public class EnemyIndicator : MonoBehaviour
    {
        public Action OnRemoveIndicator;

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Gradient _colorByDistance;
        [SerializeField] private float _minDistance, _maxDistance;

        private Transform _ownerTransform;
        private Transform _targetTransform;

        public void Initialize(Transform ownerTransform, Transform targetTransform)
        {
            _ownerTransform = ownerTransform;
            _targetTransform = targetTransform;
        }

        private void Update()
        {
            if (_targetTransform == null || _ownerTransform == null) return;
            Vector2 direction = _targetTransform.position - _ownerTransform.position;
            float distance = direction.magnitude;

            transform.up = direction.normalized;

            if (distance < _minDistance)
            {
                //투명 처리
                _sprite.color = new Color(0, 0, 0, 0);
            }
            else
            {
                Color spriteColor = _colorByDistance.Evaluate((distance - _minDistance) / (_maxDistance - _minDistance));
                _sprite.color = spriteColor;
            }
        }
    }
}
