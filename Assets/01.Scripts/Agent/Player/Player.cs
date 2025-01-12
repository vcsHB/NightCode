using Agents.Players.FSM;
using Core.EventSystem;
using InputManage;
using UnityEngine;
namespace Agents.Players
{
    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        private PlayerStateMachine _stateMachine;
        public PlayerStateMachine StateMachine => _stateMachine;
        [field: SerializeField] public GameEventChannelSO FeedbackChannel { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            InitState();
        }

        protected virtual void InitState()
        {
            
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {

            _stateMachine.UpdateState();
        }

    }
}