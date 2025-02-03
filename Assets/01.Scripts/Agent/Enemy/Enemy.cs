using Combat;
using Core.EventSystem;
using UnityEngine;
namespace Agents.Enemies
{

    public class Enemy : Agent
    {
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

        }
        protected override void HandleAgentDie()
        {

        }
    }
}