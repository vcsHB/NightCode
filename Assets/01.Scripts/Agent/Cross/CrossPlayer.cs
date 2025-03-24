using Agents.Players.FSM;
using UnityEngine;
namespace Agents.Players
{

    public class CrossPlayer : Player
    {
        private CrossPlayerStateMachine _crossPlayerStateMachine;


        protected override void InitState()
        {
            _crossPlayerStateMachine = new CrossPlayerStateMachine(this);
            _stateMachine = _crossPlayerStateMachine;
            _crossPlayerStateMachine.Initialize("Idle");

        }

        
    }
}