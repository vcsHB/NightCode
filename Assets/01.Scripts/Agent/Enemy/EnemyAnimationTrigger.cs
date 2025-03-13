using Agents.Animate;
using UnityEngine;

namespace Agents.Enemies
{
    public class EnemyAnimationTrigger : AnimationTrigger
    {
        [SerializeField] private EnemyAttackController _attackController;

        public void Attack1()
        {
            _attackController.Attack();
        }
    }

}