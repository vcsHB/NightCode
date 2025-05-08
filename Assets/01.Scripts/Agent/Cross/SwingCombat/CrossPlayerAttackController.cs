using System.Collections;
using System.Collections.Generic;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players
{

    public class CrossPlayerAttackController : MonoBehaviour
    {
        public UnityEvent OnShootEvent;
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private float _targetDetectRange;
        [SerializeField] private float _bulletSpread = 0.03f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private int _bulletAmount = 20;
        [SerializeField] private int _maxTargetAmount = 3;
        [SerializeField] private float _fireTerm = 0.05f;

        private WaitForSeconds _waitForFireTerm;
        private Coroutine _shootingCoroutine;

        private Collider2D[] _targets;
        private List<Transform> _filteredTargets = new();

        private void Awake()
        {
            _waitForFireTerm = new WaitForSeconds(_fireTerm);
        }

        public void Attack()
        {
            if (_shootingCoroutine != null) return;
            DetectTarget();
            _shootingCoroutine = StartCoroutine(AttackToTargets());
        }

        private void DetectTarget()
        {
            _filteredTargets.Clear();
            _targets = Physics2D.OverlapCircleAll(transform.position, _targetDetectRange, _targetLayer);
            for (byte i = 0; i < _targets.Length; i++)
            {
                Transform targetTrm = _targets[i].transform;
                Vector2 direction = targetTrm.position - targetTrm.position;


                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _targetDetectRange, _obstacleLayer);
                if (hit.collider == null)
                    _filteredTargets.Add(targetTrm);
            }
        }

        private IEnumerator AttackToTargets()
        {
            if (_filteredTargets.Count <= 0) yield break;
            for (int i = 0; i < _bulletAmount; i++)
            {
                int index = i % _filteredTargets.Count;
                Transform targetTrm = _filteredTargets[index];
                Vector2 direction = targetTrm.position - transform.position;

                _shooter.FireProjectile((direction + (Random.insideUnitCircle * _bulletSpread)).normalized);
                OnShootEvent?.Invoke();
                yield return _waitForFireTerm;
            }
            _shootingCoroutine = null;
        }


    }
}