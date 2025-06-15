using Agents.Animate;
using UnityEngine;
using UnityEngine.Events;

namespace Agents.Enemies
{
    public class EnemyAnimationTrigger : AnimationTrigger
    {
        public UnityEvent OnTargetDetectEvent;
        [SerializeField] private EnemyAttackController _attackController;

        public void Attack1()
        {
            _attackController.Attack();
        }

        public void HandleDetectTarget()
        {
            OnTargetDetectEvent?.Invoke();
        }
    }

}