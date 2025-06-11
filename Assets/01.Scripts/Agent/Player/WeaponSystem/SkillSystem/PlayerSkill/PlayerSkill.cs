using Agents.Players.WeaponSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public abstract class PlayerSkill : MonoBehaviour
    {
        public UnityEvent OnSkillUsedEvent;
        protected Player _player;
        protected PlayerCombatEnergyController _energyController;
        protected float _nextSkillUseTime;
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

        public void HandleUseSkill()
        {
            if (_player.IsActive)
            {
                if (_nextSkillUseTime < Time.time)
                {
                    if (_energyController.TryUseEnergy(Cost))
                    {
                        _nextSkillUseTime = Time.time + SkillCooltime;

                        OnSkillUsedEvent?.Invoke();
                        UseSkill();
                    }

                }
            }
        }
        protected abstract void UseSkill();
    }
}