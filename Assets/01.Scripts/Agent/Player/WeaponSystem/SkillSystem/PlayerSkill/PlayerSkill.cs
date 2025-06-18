using System;
using Agents.Players.WeaponSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public abstract class PlayerSkill : MonoBehaviour
    {
        public UnityEvent OnSkillUsedEvent;
        public event Action<float, float> OnCooltimeUpdateEvent;
        protected Player _player;
        protected PlayerCombatEnergyController _energyController;
        protected float _currentCoolTime;
        protected float SkillCooltime { get; private set; }
        protected int Cost { get; private set; }
        protected PlayerWeapon _playerWeapon;

        public virtual void Initialize(Player player, PlayerWeapon originWeapon, int cost, float cooltime)
        {
            _player = player;
            _playerWeapon = originWeapon;
            _energyController = _player.GetCompo<PlayerCombatEnergyController>();
            SkillCooltime = cooltime;
            Cost = cost;
        }
        protected virtual void Update()
        {
            UpdateCooltime();
        }
        public void HandleUseSkill()
        {
            if (_player.IsActive)
            {
                if (_currentCoolTime > SkillCooltime)
                {
                    if (_energyController.TryUseEnergy(Cost))
                    {
                        _currentCoolTime = 0f;

                        OnSkillUsedEvent?.Invoke();
                        UseSkill();
                    }

                }
            }
        }

        protected void UpdateCooltime()
        {
            _currentCoolTime += Time.deltaTime;
            OnCooltimeUpdateEvent?.Invoke(_currentCoolTime, SkillCooltime);
        }
        protected abstract void UseSkill();
    }
}