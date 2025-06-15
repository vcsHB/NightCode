using Agents.Players.WeaponSystem.Weapon.WeaponObjects;
using Combat.Casters;
using UnityEngine;
namespace Agents.Players.SkillSystem
{

    public class CresentSkill : PlayerSkill
    {
        [SerializeField] private TargetDetector _targetDetector;
        [SerializeField] private int _maxTargetAmount;
        private Caster _caster;

        private void Awake()
        {
            _caster = GetComponentInChildren<Caster>();
        }
        
        protected override void UseSkill()
        {
            Collider2D[] targets = _targetDetector.DetectAllTargets();

            for (int i = 0; i < targets.Length; i++)
            {
                _caster.ForceCast(targets[i]);
            }
        }

    }

}