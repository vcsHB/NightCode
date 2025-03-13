using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    public class CafeNPC : NPC
    {
        [Space]
        [Header("AnimParamRegion")]
        public AnimParamSO CounterParam;
        public AnimParamSO ServingParam;
        public AnimParamSO MoveParam;

        public TalkBubble talkBubble;
        public Sprite heartIcon;

        public Cafe Cafe { get; private set; }
        public Transform MoveTarget { get; private set; }
        public string NextState { get; private set; }


        private void Start()
        {
            stateMachine.AddState("Counter", "Counter", CounterParam);
            stateMachine.AddState("Serving", "Serving", ServingParam);
            stateMachine.AddState("Move", "Move", MoveParam);
            stateMachine.ChangeState("Counter");
        }

        public void SetNextState(string state)
            => NextState = state;

        public void SetMoveTarget(Transform target)
            => MoveTarget = target;

        public void Init(Cafe cafe)
            => Cafe = cafe;
    }
}
