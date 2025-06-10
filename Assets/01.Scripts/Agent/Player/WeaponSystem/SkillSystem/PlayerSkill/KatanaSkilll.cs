using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.SkillSystem
{

    public class KatanaSkilll : PlayerSkill
    {
        private Caster _caster;

        [SerializeField] private int _maxTargetAmount = 5;
        [SerializeField] private TargetDetector _targetDetector;

        private void Awake()
        {
            _caster = GetComponentInChildren<Caster>();
        }

        protected override void UseSkill()
        {
            Collider2D[] targets;
            _targetDetector.DetectTargetsSorted(out targets);
            if (targets.Length <= 0) return;

            for (int i = 0; i < targets.Length; i++)
            {
                if (i == _maxTargetAmount) return;

                Collider2D target = targets[i];
                _caster.ForceCast(target);
            }
        }
    }
}