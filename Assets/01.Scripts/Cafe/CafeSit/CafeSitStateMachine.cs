using System.Collections.Generic;
using UnityEngine;

namespace Base.Cafe
{
    public class CafeSitStateMachine
    {
        private Dictionary<ECafeSitState, CafeSitState> stateDictionary = new();
        private CafeSitState currentState;

        public void AddState(ECafeSitState key, CafeSitState state)
        {
            stateDictionary.Add(key, state);
        }

        public void ChangeState(ECafeSitState state)
        {
            currentState?.OnExitState();
            currentState = stateDictionary[state];
            currentState.OnEnterState();
        }


        public void OnTriggerEnter()
            => currentState.OnTriggerEnter();

        public void OnTriggerExit()
            => currentState.OnTriggerExit();
    }

    public enum ECafeSitState
    {
        Empty,
        Idle,
        Wait,
        Dirty
    }
}
