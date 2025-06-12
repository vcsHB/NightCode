using System;
using Agents.Players.WeaponSystem;
using Agents.Players.WeaponSystem.Weapon;
using StatSystem;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public class GauntletSkill : PlayerSkill
    {
        public UnityEvent OnWeaponAttackWithSkillEvent;
        public UnityEvent OnSkillOverEvent;
        [SerializeField] private float _skillDuration = 5f;
        [SerializeField] private float _bonusDamage = 50f;
        private StatSO _damageStat;
        private float _currentDuration;
        private bool _isSkillEnabled;
        private GauntletWeapon _gauntletWeapon;
        private AgentStatus _agentStatus;

        public override void Initialize(Player player, PlayerWeapon originWeapon, int cost, float cooltime)
        {
            base.Initialize(player, originWeapon, cost, cooltime);

            player.OnExitEvent += HandleOverSkill;
            _gauntletWeapon = _playerWeapon as GauntletWeapon;
            _gauntletWeapon.OnAttackEvent.AddListener(HandleWeaponAttack);
            Invoke(nameof(InitStatus), 0.5f);
        }

        private void InitStatus()
        {

            _agentStatus = _player.GetCompo<AgentStatus>();
            _damageStat = _agentStatus.GetStat(StatusEnumType.Attack);
        }

        private void HandleWeaponAttack()
        {
            if (_isSkillEnabled)
            {
                OnWeaponAttackWithSkillEvent?.Invoke();
            }
        }

        protected override void Update()
        {
            base.Update();
            if (_isSkillEnabled)
            {

                _currentDuration += Time.deltaTime;
                if (_currentDuration > _skillDuration)
                {
                    _player.HealthCompo.SetResist(true);
                    HandleOverSkill();
                }
            }
        }


        protected override void UseSkill()
        {
            _damageStat.AddModifier(_bonusDamage);

        }

        private void HandleOverSkill()
        {
            _isSkillEnabled = false;
            _damageStat.RemoveModifier(_bonusDamage);
            _player.HealthCompo.SetResist(false);
            _currentDuration = 0f;
            OnSkillOverEvent?.Invoke();
        }
    }
}