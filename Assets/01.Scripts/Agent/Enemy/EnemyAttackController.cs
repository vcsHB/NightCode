using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{
    public abstract class EnemyAttackController : MonoBehaviour
    {
        public UnityEvent OnAttackEvent;
        [SerializeField] protected bool _enable;
        protected Transform _targetTrm;
        protected float _lastAttackTime;

        public void SetEnable(bool value)
        {
            _enable = value;
        }

        public virtual bool HandleAttack(Transform target)
        {
            if (_enable)
            {
                _targetTrm = target;
                Attack();
                OnAttackEvent?.Invoke();
                _lastAttackTime = Time.time;
                return true;
            }
            
            return false;
        }
        
        public abstract void Attack();

    }
}