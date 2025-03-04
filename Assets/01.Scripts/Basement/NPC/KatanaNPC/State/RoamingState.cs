using Agents.Animate;
using Unity.AppUI.Core;
using UnityEngine;

namespace Basement.NPC
{
    public class RoamingState : NPCState
    {
        private NPCSO npcSO;
        private float _roamingEnterTime;
        private float _roamingTime;
        private float _dir;

        public RoamingState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            npcSO = npc.npcSO;

            int[] dir = new int[2] { -1, 1 };
            _dir = dir[Random.Range(0, 2)];
        }

        public override void EnterState()
        {
            base.EnterState();
            _roamingEnterTime = Time.time;
            _roamingTime = npcSO.GetRandomRoamingTime();
            npc.SetDirection(_dir);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (npc.IsWallDetected())
            {
                _dir *= -1;
                stateMachine.ChangeState("Idle");
            }

            if(_roamingEnterTime + _roamingTime < Time.time)
            {
                _dir *= -1;
                stateMachine.ChangeState("Idle");
            }
        }
    }
}
