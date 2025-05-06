using Agents.Animate;
using System;
using System.Collections.Generic;

namespace Base
{
    public class BaseEntityStateMachine
    {
        private Dictionary<string, BaseEntityState> _stateDic;
        private BaseEntity _entity;
        public BaseEntityState currentState { get; private set; }
        public string currentStateString { get; private set; }

        public BaseEntityStateMachine(BaseEntity entity)
        {
            _entity = entity;
            _stateDic = new Dictionary<string, BaseEntityState>();
        }

        public void AddState(string name, string typeName, AnimParamSO animParam)
        {
            Type type = Type.GetType(typeName);
            BaseEntityState state = Activator.CreateInstance(type, _entity, animParam) as BaseEntityState;
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
