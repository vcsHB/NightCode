using Combat;
using Core.EventSystem;
using Unity.Behavior;
using UnityEngine;
namespace Agents.Enemies
{

    public class Enemy : Agent
    {
        [SerializeField] protected LayerMask _whatIsTarget;
        protected BehaviorGraphAgent _btAgent;
        public Health HealthCompo { get; protected set; }
        public Rigidbody2D RigidCompo { get; protected set; }
        [field: SerializeField] public GameEventChannelSO EventChannel { get; private set; }

        protected override void Awake()
        {
            EventChannel = Instantiate(EventChannel);
            base.Awake();
            RigidCompo = GetComponent<Rigidbody2D>();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandleAgentDie);

            _btAgent = GetComponent<BehaviorGraphAgent>();
        }
        // protected override void HandleAgentDie()
        // {
        //     IsDead = true;
        // }

        public BlackboardVariable<T> GetVariable<T>(string variableName)
        {
            if (_btAgent.GetVariable(variableName, out BlackboardVariable<T> variable))
            {
                return variable;
            }
            return null;
        }

        public void SetVariable<T>(string variableName, T value)
        {
            BlackboardVariable<T> variable = GetVariable<T>(variableName);
            Debug.Assert(variable != null, $"Variable {variableName} not found");
            variable.Value = value;
        }

        public Transform GetTargetInRadius(float radius)
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, _whatIsTarget);
            return collider != null ? collider.transform : null;
        }
    }
}