using System;
using UnityEngine;
namespace  Agents.Players.WeaponSystem
{
    // It works for Normal Attack
    public abstract class PlayerWeapon : MonoBehaviour
    {
        protected Player _player;
        protected PlayerAnimationTrigger _animationTrigger;


        public abstract void HandleAttack();

        public virtual void Initialize(Player player)
        {
            _player = player;
            _animationTrigger = _player.GetCompo<PlayerAnimationTrigger>();

        }
    }
}