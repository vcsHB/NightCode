
using System;
using Agents.Enemies.BossManage;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace Agents.Enemies.BT.Event
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/StateChange")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "StateChange", message: "change to [state]", category: "Events", id: "1231")]
    public partial class BurnOutStateChange : EventChannelBase
    {
        public delegate void StateChangeEventHandler(BurnOutStateEnum state);
        public event StateChangeEventHandler Event;

        public void SendEventMessage(BurnOutStateEnum state)
        {
            Event?.Invoke(state);
            //Debug.Log("SendEvent 호출됨: " + state);
        }

        public override void SendEventMessage(BlackboardVariable[] messageData)
        {
            BlackboardVariable<BurnOutStateEnum> stateBlackboardVariable = messageData[0] as BlackboardVariable<BurnOutStateEnum>;
            var state = stateBlackboardVariable != null ? stateBlackboardVariable.Value : default(BurnOutStateEnum);

            Event?.Invoke(state);
            //Debug.Log("SendEvent 호출됨: " + state);
        }

        public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
        {
            StateChangeEventHandler del = (state) =>
            {
                BlackboardVariable<BurnOutStateEnum> var0 = vars[0] as BlackboardVariable<BurnOutStateEnum>;
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