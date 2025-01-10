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
        [field: SerializeField] public GameEventChannelSO CreateFeedbackChannel { get; private set; }
        [field: SerializeField] public GameEventChannelSO FinishFeedbackChannel { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {

            _stateMachine.UpdateState();
        }

    }
}