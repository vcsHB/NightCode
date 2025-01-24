using Agents.Players.FSM;
using UnityEngine;
namespace Agents.Players
{
    public class KatanaPlayer : Player
    {
        private KatanaPlayerStateMachine _katanaPlayerStateMachine;

        protected override void InitState()
        {
            _katanaPlayerStateMachine = new KatanaPlayerStateMachine(this);
            _stateMachine = _katanaPlayerStateMachine;
            _katanaPlayerStateMachine.Initialize("Idle");

        }

    }
}