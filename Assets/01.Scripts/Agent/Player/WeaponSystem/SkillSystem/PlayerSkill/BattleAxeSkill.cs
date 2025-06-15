using Agents.Players.WeaponSystem;
using Agents.Players.WeaponSystem.Weapon;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players.SkillSystem
{

    public class BattleAxeSkill : PlayerSkill
    {
        public UnityEvent OnSkillOverEvent;
        private BattleAxeWeapon _battleAxe;
        [SerializeField] private float _skillDuration = 10f;
        [Header("BattleAxe Increase Values")]
        [SerializeField] private float _flyDistanceMultiplier = 1.5f;
        [SerializeField] private float _flySpeedMultiplier = 2f;
        private float _currentDuration;
        private bool _isSkillEnabled;

        public override void Initialize(Player player, PlayerWeapon originWeapon, int cost, float cooltime)
        {
            base.Initialize(player, originWeapon, cost, cooltime);
            _battleAxe = _playerWeapon as BattleAxeWeapon;
            player.OnExitEvent += HandleOverSkill;
        }

        protected override void Update()
        {
            base.Update();
            if (_isSkillEnabled)
            {

                _currentDuration += Time.deltaTime;
                if (_currentDuration > _skillDuration)
                {
                    HandleOverSkill();
                }
            }
        }

        protected override void UseSkill()
        {
            _battleAxe.SetAxeSpeed(_flySpeedMultiplier, _flyDistanceMultiplier);
            _isSkillEnabled = true;
            _currentDuration = 0f;
        }

        private void HandleOverSkill()
        {
            _isSkillEnabled = false;
            _currentDuration = 0f;
            _battleAxe.SetAxeSpeed();
            OnSkillOverEvent?.Invoke();
        }
    }
}