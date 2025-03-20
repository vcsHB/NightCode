using Agents.Players.FSM;
using UnityEngine;
namespace Agents.Players
{
    public class CresentPlayer : Player
    {
        private CresentPlayerStateMachine _cresentPlayerStateMachine;
        [field: SerializeField] public float DashAttackStandardVelocity { get; set; } = 50f;
        protected override void InitState()
        {
            _cresentPlayerStateMachine = new CresentPlayerStateMachine(this);
            _stateMachine = _cresentPlayerStateMachine;
            _cresentPlayerStateMachine.Initialize("Idle");
        }

        public override void EnterCharacter()
        {
            base.EnterCharacter();
        }

        public override void ExitCharacter()
        {
            base.ExitCharacter();

        }
    }
}