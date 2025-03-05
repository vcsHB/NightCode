using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement.NPC
{
    public class NPCStateMachine
    {
        private Dictionary<string, NPCState> _stateDic;
        private NPC _npc;
        public NPCState currentState { get; private set; }

        public NPCStateMachine(NPC npc)
        {
            _npc = npc;
            _stateDic = new Dictionary<string, NPCState>();
        }

        public void AddState(string name, string typeName, AnimParamSO animParam)
        {
            Type type = Type.GetType($"Basement.NPC.{typeName}State");
            NPCState state = Activator.CreateInstance(type, _npc, animParam) as NPCState;
            _stateDic.Add(name, state);
        }

        public void ChangeState(string name)
        {
            currentState?.ExitState();
            currentState = _stateDic[name];
            currentState?.EnterState();
        }
    }
}
