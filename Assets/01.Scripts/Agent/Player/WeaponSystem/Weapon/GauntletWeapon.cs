using System;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon
{


    public class GauntletWeapon : PlayerWeapon
    {
        [SerializeField] private Caster _attackCaster;
        [SerializeField] private Caster _projectileParryCaster;
        private bool _isAttackEnabled;

        public override void Initialize(Player player, int cost)
        {
            base.Initialize(player, cost);
            _animationTrigger.OnRopeShootEvent.AddListener(HandleRopeShoot);
            _animationTrigger.OnRopeRemoveEvent.AddListener(HandleRopeRemove);
            _animationTrigger.OnGroundPullArriveEvent.AddListener(HandleAttack);
        }

        private void HandleRopeShoot()
        {
            _isAttackEnabled = true;
        }

        private void HandleRopeRemove()
        {
            _isAttackEnabled = false;
        }

        protected override void Attack()
        {
            _attackCaster.Cast();
        }

        private void Update()
        {
            if (_isAttackEnabled)
            {
                _projectileParryCaster.Cast();
            }
        }
    }
}