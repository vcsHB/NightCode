using Agents.Enemies.BT.Event;
using UnityEngine;
namespace Agents.Enemies.BossManage
{

    public class BurnOutBoss : Boss
    {
        private BurnOutBossRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponentInChildren<BurnOutBossRenderer>();
        }

        private void Start()
        {
        }
        protected override void HandleAgentDie()
        {
            base.HandleAgentDie();
            GetVariable<BurnOutStateChange>("StateChange").Value.SendEventMessage(BurnOutStateEnum.Dead);

        }
    }
}