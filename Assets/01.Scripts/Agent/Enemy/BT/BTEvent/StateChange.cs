using Agents.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using Agents.Enemies.Highbinders;

namespace Agents.Enemies.BT.Event
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "StateChange", message: "change to [state]", category: "Events", id: "ba535b503dd964d3fa75a8c52eb754b9")]
    public partial class StateChange : EventChannelBase
    {
        public delegate void StateChangeEventHandler(HighbinderStateEnum state);
        public event StateChangeEventHandler Event;

        public void SendEventMessage(HighbinderStateEnum state)
        {
            Event?.Invoke(state);
        }

        public override void SendEventMessage(BlackboardVariable[] messageData)
        {
            BlackboardVariable<HighbinderStateEnum> stateBlackboardVariable = messageData[0] as BlackboardVariable<HighbinderStateEnum>;
            var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(HighbinderStateEnum);

            Event?.Invoke(state);
        }

        public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
        {
            StateChangeEventHandler del = (state) =>
            {
                BlackboardVariable<HighbinderStateEnum> var0 = vars[0] as BlackboardVariable<HighbinderStateEnum>;
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

