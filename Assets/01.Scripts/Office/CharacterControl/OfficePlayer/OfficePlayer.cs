using Agents;
using InputManage;
using UnityEngine;

namespace Office.CharacterControl
{

    public class OfficePlayer : Agent
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
        public OfficePlayerStateMachine StateMachine { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            PlayerInput.SetEnabledAllStatus();
            StateMachine = new OfficePlayerStateMachine(this);
            StateMachine.InitializeState();
        }

    }

}