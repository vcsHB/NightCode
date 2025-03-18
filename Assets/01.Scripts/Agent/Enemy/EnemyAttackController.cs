using Unity.Behavior;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{
    public abstract class EnemyAttackController : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnAttackEvent;
        [SerializeField] protected bool _enable;
        protected BlackboardVariable<Transform> _targetVariable;
        protected Transform _targetTrm;
        protected float _lastAttackTime;
        protected Enemy _owner;

        protected virtual void Start()
        {
            _targetVariable = _owner.GetVariable<Transform>("Target");
        }

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

        public void Initialize(Agent agent)
        {
            _owner = agent as Enemy;
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}