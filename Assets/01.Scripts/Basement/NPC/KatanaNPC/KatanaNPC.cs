using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class KatanaNPC : NPC
    {
        [Space(15)]
        [Header("AnimParamRegion")]
        public AnimParamSO IdleParam;
        public AnimParamSO RoamingParam;
        public AnimParamSO EmotionParam;

        protected override void Awake()
        {
            base.Awake();

            stateMachine.AddState("Idle", "Idle", IdleParam);
            stateMachine.AddState("Roaming", "Roaming", RoamingParam);
            stateMachine.AddState("Emotion", "Emotion", EmotionParam);
            stateMachine.ChangeState("Idle");
        }
    }
}
