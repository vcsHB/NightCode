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
        public bool CanCharacterChange { get; private set; }
        public bool IsActive { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            FeedbackChannel = Instantiate(FeedbackChannel);
            RigidCompo = GetComponent<Rigidbody2D>();
            HealthCompo = GetComponent<Health>();
            HealthCompo.OnDieEvent.AddListener(HandlePlayerDieEvent);
            
            InitState();
        }

        private void HandlePlayerDieEvent()
        {
            IsDead = true;
            _stateMachine.ChangeState("Dead");
        }

        private void Start()
        {
            StateMachine.StartState();
        }
        protected virtual void InitState()
        {
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {
            if(IsActive)
                _stateMachine.UpdateState();
        }
        public void SetActive(bool value)
        {
            IsActive = value;
        }

        public void EnterCharacter()
        {
            //gameObject.SetActive(true);
            _stateMachine.ChangeState("Enter");
        }
        public void ExitCharacter()
        {
            _stateMachine.ChangeState("Exit");
            //gameObject.SetActive(false);
        }

    }
}