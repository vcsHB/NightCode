using Agents;
using Combat;
using StatSystem;
using UnityEngine;

namespace EffectSystem
{
    public class EffectAcid : EffectState
    {
        [SerializeField] private int _damageDuration;
        private int _currentDuration;
        private StatSO _moveSpeedStat;
        [SerializeField] private float _increaseRate = 0.2f;
        private bool _isAcidEnabled;
        private float _reducedMoveSpeed;

        public override void Initialize(Agent agent, bool isResist)
        {
            base.Initialize(agent, isResist);
            AgentStatus statCompo = agent.GetCompo<AgentStatus>();
            _moveSpeedStat = statCompo.GetStat(StatusEnumType.Speed);
        }
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            if (_isAcidEnabled)
            {
                _owner.HealthCompo.ApplyDamage(new CombatData()
                {
                    damage = 2,
                    type = AttackType.Effect
                });
                _currentDuration--;
                if (_currentDuration <= 0)
                {
                    Over();
                    _isAcidEnabled = false;
                }
                return;
            }

        }

        public override void Apply(int stack = 1, int level = 1, float percent = 1)
        {
            if (_isAcidEnabled) return;
            base.Apply(stack, level, percent);

        }

        public override void Over()
        {
            base.Over();
            _moveSpeedStat.RemoveModifier(_reducedMoveSpeed);
        }
        protected override void OnBurstEffectStack()
        {
            base.OnBurstEffectStack();
            _isAcidEnabled = true;
            _currentDuration = _damageDuration;
            _reducedMoveSpeed = -_moveSpeedStat.Value * _increaseRate;
            _moveSpeedStat.AddModifier(_reducedMoveSpeed);
        }
        public override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Acid;
        }
    }
}