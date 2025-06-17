using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;

namespace Agents.Players.ChipsetSystem.ChipsetObjects
{
    public class MicroDrone : MonoBehaviour
    {
        [Header("Tracking Settings")]
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _followSpeed = 5f;
        [SerializeField] private float _followDeadzone = 0.5f;

        [Header("Attack Settings")]
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private float _attackTerm = 1f;
        [SerializeField] private ProjectileShooter _projectileShooter;

        private float _attackTimer;

        private void Update()
        {
            Follow();
            HandleAttackCycle();
        }

        public void SetFollowTarget(Transform followTarget)
        {
            
        }


        private void Follow()
        {
            if (_followTarget == null) return;

            Vector2 direction = _followTarget.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= _followDeadzone) return;

            // 감속 처리
            float t = Mathf.Clamp01(distance / (_followDeadzone * 4f)); // 거리멀수록 빠르게
            float speed = Mathf.Lerp(0f, _followSpeed, t);

            Vector2 move = direction.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)move;
        }

        private void HandleAttackCycle()
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackTerm)
            {
                Attack();
                _attackTimer = 0f;
            }
        }

        private void Attack()
        {
            Collider2D target = _targetDetector.DetectClosestTarget();
            if (target == null) return;

            Vector2 direction = (target.transform.position - transform.position).normalized;
            _projectileShooter.FireProjectile(direction);
        }
    }
}
