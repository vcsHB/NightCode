using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "MoveToStatePosition", story: "move to [State] Position with [Mover]", category: "Action", id: "edf9cea6cab041d991e63d98b5313d49")]
    public partial class MoveToStatePositionAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutStateEnum> State;
        [SerializeReference] public BlackboardVariable<BurnOutBossMovement> Mover;

        private bool _isInitialized;

        protected override Status OnStart()
        {
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }
        

    }


}