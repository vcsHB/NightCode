using Unity.Behavior;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Enemies
{
    public abstract class EnemyAttackController : MonoBehaviour, IAgentComponent
    {
        public UnityEvent OnAttackEvent;

        public event System.Action OnAttackEndEvent;
        [SerializeField] protected bool _enable;
        protected BlackboardVariable<Transform> _targetVariable;
        protected Transform _targetTrm;
        protected float _lastAttackTime;
        protected Enemy _owner;
        protected EnemyAnimationTrigger _animTrigger;

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
        public virtual void HandleDetectTarget()
        {

        }

        protected void InvokeAttackEnd()
        {
            OnAttackEndEvent?.Invoke();
        }


        public virtual void Initialize(Agent agent)
        {
            _owner = agent as Enemy;
            _animTrigger = _owner.GetCompo<EnemyAnimationTrigger>();
            _animTrigger.OnTargetDetectEvent.AddListener(HandleDetectTarget);

        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}