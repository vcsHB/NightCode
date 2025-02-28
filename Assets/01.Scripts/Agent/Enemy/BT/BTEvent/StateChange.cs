using Agents.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

namespace Agents.Enemies.BT.Event
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "StateChange", message: "change to [state]", category: "Events", id: "ba535b503dd964d3fa75a8c52eb754b9")]
    public partial class StateChange : EventChannelBase
    {
        public delegate void StateChangeEventHandler(HighBinderStateEnum state);
        public event StateChangeEventHandler Event;

        public void SendEventMessage(HighBinderStateEnum state)
        {
            Event?.Invoke(state);
        }

        public override void SendEventMessage(BlackboardVariable[] messageData)
        {
            BlackboardVariable<HighBinderStateEnum> stateBlackboardVariable = messageData[0] as BlackboardVariable<HighBinderStateEnum>;
            var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(HighBinderStateEnum);

            Event?.Invoke(state);
        }

        public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
        {
            StateChangeEventHandler del = (state) =>
            {
                BlackboardVariable<HighBinderStateEnum> var0 = vars[0] as BlackboardVariable<HighBinderStateEnum>;
                if (var0 != null)
                    var0.Value = state;

                callback();
            };
            return del;
        }

        public override void RegisterListener(Delegate del)
        {
            Event += del as StateChangeEventHandler;
        }

        public override void UnregisterListener(Delegate del)
        {
            Event -= del as StateChangeEventHandler;
        }
    }

}

