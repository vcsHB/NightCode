    using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerStateMachine : PlayerStateMachine
    {
        public KatanaPlayerStateMachine(Player player) : base(player)
        {
        }

        public override void Initialize(string firstState)
        {
            AddState("");


            base.Initialize(firstState);
        }
    }
}
