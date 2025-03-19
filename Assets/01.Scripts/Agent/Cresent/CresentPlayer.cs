using Agents.Players.FSM;
using UnityEngine;
namespace Agents.Players
{

    public class CresentPlayer : Player
    {
        private CresentPlayerStateMachine _cresentPlayerStateMachine;
        protected override void InitState()
        {
            _cresentPlayerStateMachine = new CresentPlayerStateMachine(this);
            _stateMachine = _cresentPlayerStateMachine;
            _cresentPlayerStateMachine.Initialize("Idle");

        }
    }
}