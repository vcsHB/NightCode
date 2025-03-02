using Combat;
using UnityEngine;
namespace Agents.Enemies.Highbinders
{
    public class HighbinderAttackController : EnemyAttackController
    {
        [SerializeField] private Caster _combatCaster;

        public override void Attack()
        {
            _combatCaster.Cast();
        }
    }
}