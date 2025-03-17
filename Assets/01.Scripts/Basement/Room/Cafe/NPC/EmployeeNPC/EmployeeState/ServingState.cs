using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class ServingState : NPCState
    {
        private float _enterTime;

        public ServingState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {

        }

        public override void EnterState()
        {
            base.EnterState();

            _enterTime = Time.time;

        }

        public override void UpdateState()
        {
            base.UpdateState();


        }


        //이건 나중에 애니메이션 추가하고 나서하는걸루
        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            stateMachine.ChangeState("Service");
        }
    }
}
