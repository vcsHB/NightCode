using System.Collections;
using Combat.CombatObjects.ProjectileManage;
using UnityEditor.Build;
using UnityEngine;
namespace Agents.Enemies.Highbinders
{

    public class HighbinderLaserController : EnemyAttackController
    {
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private LaserAimLine _aimLine;
        [SerializeField] private float _attackTerm = 4f;
        private bool _isUpdateDirection;
        private Vector2 _attackDirection;
        public override void HandleDetectTarget()
        {
            base.HandleDetectTarget();
            _isUpdateDirection = true;
            _aimLine.SetLineEnable(true);
        }
        private void Update()
        {
            if (_targetVariable.Value == null || !_isUpdateDirection) return;
            Vector2 targetPosition = _targetVariable.Value.position;
            _attackDirection = targetPosition - (Vector2)_owner.transform.position;
            _attackDirection.Normalize();
            _aimLine.SetDirection(_attackDirection);

        }

        public override void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            _isUpdateDirection = false;
            yield return new WaitForSeconds(_attackTerm);
            FireLaser();
            InvokeAttackEnd();
            _aimLine.SetLineEnable(false);
        }

        private void FireLaser()
        {

            _shooter.SetDirection(_attackDirection * 20f + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
            _shooter.SetDirection(_attackDirection * 20f + (Vector2)Random.insideUnitSphere);
            _shooter.FireProjectile();
        }
    }
}