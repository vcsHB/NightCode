using System;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.WeaponSystem.Weapon
{


    public class CresentWeapon : PlayerWeapon
    {
        [SerializeField] private Caster _attackCaster;
        [SerializeField] private Caster _projectileParryCaster;
        private bool _isAttackEnabled;
     
        public override void Initialize(Player player)
        {
            base.Initialize(player);
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

        public override void HandleAttack()
        {
            print("asd");
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