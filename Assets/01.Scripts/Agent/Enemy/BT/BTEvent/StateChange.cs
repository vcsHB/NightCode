using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using Agents.Enemies.Bat;

namespace Agents.Enemies.BT.Event
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "StateChange", message: "change to [state]", category: "Events", id: "e1892734981")]
    public partial class BatStateChange : EventChannelBase
    {
        public delegate void StateChangeEventHandler(BatStateEnum state);
        public event StateChangeEventHandler Event;

        public void SendEventMessage(BatStateEnum state)
        {
            Event?.Invoke(state);
            //Debug.Log("SendEvent 호출됨: " + state);
        }

        public override void SendEventMessage(BlackboardVariable[] messageData)
        {
            BlackboardVariable<BatStateEnum> stateBlackboardVariable = messageData[0] as BlackboardVariable<BatStateEnum>;
            var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(BatStateEnum);

            Event?.Invoke(state);
            //Debug.Log("SendEvent 호출됨: " + state);
        }

        public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
        {
            StateChangeEventHandler del = (state) =>
            {
                BlackboardVariable<BatStateEnum> var0 = vars[0] as BlackboardVariable<BatStateEnum>;
                if (var0 != null)
                    var0.Value = state;
                else
                    Debug.Log("엥 Enum이 널인디");

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

