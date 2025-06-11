using System.Collections;
using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.CombatObjects.ProjectileManage;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public class ShotgunSkill : PlayerSkill
    {
        public UnityEvent OnShotgunFireEvent;
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private ShotgunFireVFX _vfx;
        [SerializeField] private int _attackAmount = 3;
        [SerializeField] private ShotgunProjectileShooter _shotgunProjectileShooter;
        [SerializeField] private float _fireTerm;
        private WaitForSeconds _waitForFireTerm;
        private void Awake()
        {
            _waitForFireTerm = new WaitForSeconds(_fireTerm);
        }
        protected override void UseSkill()
        {

            StartCoroutine(SkillCoroutine());
        }

        private IEnumerator SkillCoroutine()
        {
            Collider2D target = _targetDetector.DetectClosestTarget();
            for (int i = 0; i < _attackAmount; i++)
            {
                Vector2 direction = target.transform.position - _player.transform.position;
                _shotgunProjectileShooter.FireShotgun(direction);
                _vfx.Play(direction);
                OnShotgunFireEvent?.Invoke();
                yield return _waitForFireTerm;
            }
        }
    }
}