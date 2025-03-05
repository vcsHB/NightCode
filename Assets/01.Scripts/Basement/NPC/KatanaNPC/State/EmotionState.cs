using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class EmotionState : NPCState
    {
        private NPCSO _npcSO;
        private int _hash;

        public EmotionState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _npcSO = npc.npcSO;
        }

        public override void EnterState()
        {
            _hash = npc.curAnimParam;
            npcRenderer.SetAnimParam(_hash, true);
        }

        public override void ExitState()
        {
            npcRenderer.SetAnimParam(_hash, false);
        }

        public override void OnTriggerEnter()
        {
            base.OnTriggerEnter();
            stateMachine.ChangeState("Roaming");
        }
    }
}
