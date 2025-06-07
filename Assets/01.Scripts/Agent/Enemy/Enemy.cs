using Combat;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agents.Enemies
{

    public class Enemy : Agent
    {
        public Action<Enemy, string> OnDisableBody;
        [SerializeField] protected LayerMask _whatIsTarget;
        [SerializeField] protected LayerMask _whatIsObstacle;
        protected BehaviorGraphAgent _btAgent;
        protected string _enemyName;

        public string EnemyName => _enemyName;
        public Rigidbody2D RigidCompo { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            RigidCompo = GetComponent<Rigidbody2D>();

            _btAgent = GetComponent<BehaviorGraphAgent>();
        }

        public virtual void Init(string name)
        {
            _enemyName = name;
        }



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

        public Transform GetObstacleInDirection(Vector2 direction, float distance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, _whatIsObstacle);
            return hit.transform;

        }
    }
}