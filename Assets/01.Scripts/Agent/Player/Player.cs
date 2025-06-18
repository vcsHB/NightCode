using System;
using Agents.Players.FSM;
using Combat;
using InputManage;
using UnityEngine;
namespace Agents.Players
{
    public class Player : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        protected PlayerStateMachine _stateMachine;
        public PlayerStateMachine StateMachine => _stateMachine;

        public Rigidbody2D RigidCompo { get; protected set; }
        [field: SerializeField] public Transform RopeHolder { get; private set; }
        public bool CanCharacterChange { get; set; } = true;
        [field: SerializeField] public bool IsActive { get; private set; }
        public event Action OnEnterEvent;
        public event Action OnExitEvent;
        private bool _startDisable;
        [field: SerializeField] public int ID { get; private set; }
        public void SetPersonalId(int id)
        {
            ID = id;
        }

        protected override void Awake()
        {
            base.Awake();
            RigidCompo = GetComponent<Rigidbody2D>();
            HealthCompo.OnHealthChangedEvent.AddListener(HandlePlayerHit);

            InitState();
        }
        protected override void Start()
        {
            base.Start();
            StateMachine.StartState();

            if (_startDisable) gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            _stateMachine.CurrentState.Exit();
        }

        protected virtual void InitState()
        {
            _stateMachine = new PlayerStateMachine(this);
            _stateMachine.Initialize("Idle");
        }

        private void Update()
        {
            if (IsActive)
                _stateMachine.UpdateState();
        }
        public void SetActive(bool value)
        {
            IsActive = value;
        }

        public virtual void SetIdleEnter()
        {
            _stateMachine.ChangeState("Idle");
            OnEnterEvent?.Invoke();

        }
        public virtual void EnterCharacter()
        {
            _stateMachine.ChangeState("Enter");
            OnEnterEvent?.Invoke();

        }
        public virtual void ExitCharacter()
        {
            _stateMachine.ChangeState("Exit");
            OnExitEvent?.Invoke();
        }

        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();

            _stateMachine.ChangeState("Dead");
        }

        protected void HandlePlayerHit()
        {
            //_stateMachine.ChangeState("Swing");
        }

        public void SetStartDisable(bool value) => _startDisable = value;
    }
}