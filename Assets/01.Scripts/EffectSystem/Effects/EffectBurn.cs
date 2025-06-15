using Agents;
using Combat;
using UnityEngine;

namespace EffectSystem
{
    public class EffectBurn : EffectState
    {
        [SerializeField] private int _maxStack = 30;
        public override void Apply(int stack = 1, int level = 1, float percent = 1)
        {

            base.Apply(stack, level, percent);
            currentEffectStack = Mathf.Clamp(currentEffectStack, 0, _maxStack);
        }
        public override void UpdateBySecond()
        {
            base.UpdateBySecond();
            ReduceStack();
            _ownerHealth.ApplyDamage(new CombatData()
            {
                damage = level * currentEffectStack,
                type = AttackType.Heat

            });
        }
        protected override void ReduceStack(int amount = 1)
        {
            base.ReduceStack(amount);
            if (currentEffectStack <= 0)
                Over();

        }

        public override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Burn;
        }
    }
}