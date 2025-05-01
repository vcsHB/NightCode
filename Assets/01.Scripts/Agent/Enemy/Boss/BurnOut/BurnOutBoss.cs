using Agents.Enemies.BT.Event;
using UnityEngine;
namespace Agents.Enemies.BossManage
{

    public class BurnOutBoss : Boss
    {
        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();

            //GetVariable<BurnOutStateChange>("StateChange").Value.SendEventMessage(BurnOutStateEnum);

        }
    }
}