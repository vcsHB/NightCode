using Agents.Enemies.BT.Event;
using UnityEngine;

namespace Agents.Enemies.Bat
{

    public class BatEnemy : Enemy
    {
        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();
            BatStateChange stateChange = GetVariable<BatStateChange>("StateChange");
            stateChange.SendEventMessage(BatStateEnum.Dead);

        }
    }


}