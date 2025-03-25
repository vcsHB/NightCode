using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    public class CafeEntityStateMachine
    {
        private Dictionary<string, CafeEntityState> _stateDic;
        private CafeEntity _npc;
        public CafeEntityState currentState { get; private set; }
        public string currentStateString { get; private set; }

        public CafeEntityStateMachine(CafeEntity npc)
        {
            _npc = npc;
            _stateDic = new Dictionary<string, CafeEntityState>();
        }

        public void AddState(string name, string typeName, AnimParamSO animParam)
        {
            Type type = Type.GetType(typeName);
            CafeEntityState state = Activator.CreateInstance(type, _npc, animParam) as CafeEntityState;
            _stateDic.Add(name, state);
        }

        public void ChangeState(string name)
        {
            currentStateString = name;
            currentState?.ExitState();
            currentState = _stateDic[name];
            currentState?.EnterState();
        }
    }
}
