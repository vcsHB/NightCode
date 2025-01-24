using System;
using Agents.Players.FSM;
using Core.EventSystem;
using InputManage;
using UnityEngine;
namespace Agents.Players
{
    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        protected PlayerStateMachine _stateMachine;
        public PlayerStateMachine StateMachine => _stateMachine;
        [field: SerializeField] public GameEventChannelSO FeedbackChannel { get; private set; }
        public bool IsDead { get; protected set; }
        public Health HealthCompo { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandlePlayerDieEvent);
        }

        private void HandlePlayerDieEvent()
        {
            IsDead = true;
            _stateMachine.ChangeState("Dead");
        }

        private void Start()
        {
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

        public void EnterCharacter()
        {
            _stateMachine.ChangeState("Enter");

        }


        public void ExitCharacter()
        {
            _stateMachine.ChangeState("Exit");
        }

    }
}