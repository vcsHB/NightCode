using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public abstract class CafeEntityState
    {
        public CafeEntity npc;
        public CafeEntityStateMachine stateMachine;
        public CafeEntityRenderer npcRenderer;

        protected AnimParamSO _stateAnimParam;
        protected bool _isTriggered;
        protected int _animHash;

        public CafeEntityState(CafeEntity npc, AnimParamSO animParamSO)
        {
            this.npc = npc;
            npcRenderer = npc.npcRenderer;
            stateMachine = npc.stateMachine;
            _stateAnimParam = animParamSO;
            _animHash = _stateAnimParam.hashValue;
        }

        public virtual void EnterState()
        {
            _isTriggered = false;
            npcRenderer.SetAnimParam(_animHash, true);
        }

        public virtual void UpdateState()
        {

        }

        public virtual void ExitState()
        {
            npcRenderer.SetAnimParam(_animHash, false);
        }

        public virtual void OnTriggerEnter()
        {
            _isTriggered = true;
        }
    }
}
