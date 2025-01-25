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
        public Rigidbody2D RigidCompo { get; protected set; }
        [field: SerializeField] public Transform RopeHolder { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            FeedbackChannel = Instantiate(FeedbackChannel);
            RigidCompo = GetComponent<Rigidbody2D>();
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
            //_stateMachine.ChangeState("Enter");
            gameObject.SetActive(true);
        }
        public void ExitCharacter()
        {
            gameObject.SetActive(false);
            //_stateMachine.ChangeState("Exit");
        }

    }
}