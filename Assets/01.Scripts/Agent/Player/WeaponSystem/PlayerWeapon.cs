using System;
using UnityEngine;
namespace Agents.Players.WeaponSystem
{
    // It works for Normal Attack
    public abstract class PlayerWeapon : MonoBehaviour
    {
        protected Player _player;
        protected PlayerAnimationTrigger _animationTrigger;
        protected PlayerCombatEnergyController _energyController;
        protected int Cost { get; private set; }

        /// <summary>
        /// Subscribe this function to an External Action Events
        /// </summary>
        public void HandleAttack()
        {
            if (_energyController.TryUseEnergy(Cost))
            {

                Attack();
            }
        }
        protected abstract void Attack();

        public virtual void Initialize(Player player, int cost)
        {
            _player = player;
            _animationTrigger = _player.GetCompo<PlayerAnimationTrigger>();
            _energyController = _player.GetCompo<PlayerCombatEnergyController>();
            Cost = cost;

        }
    }
}