using Agents.Animate;
using System;
using System.Collections.Generic;

namespace Base
{
    public class AvatarEntityStateMachine
    {
        private Dictionary<string, AvatarEntityState> _stateDic;
        private AvatarEntity _entity;
        public AvatarEntityState currentState { get; private set; }
        public string currentStateString { get; private set; }

        public AvatarEntityStateMachine(AvatarEntity entity)
        {
            _entity = entity;
            _stateDic = new Dictionary<string, AvatarEntityState>();
        }

        public void AddState(string name, string typeName, AnimParamSO animParam)
        {
            Type type = Type.GetType(typeName);
            AvatarEntityState state = Activator.CreateInstance(type, _entity, animParam) as AvatarEntityState;
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
