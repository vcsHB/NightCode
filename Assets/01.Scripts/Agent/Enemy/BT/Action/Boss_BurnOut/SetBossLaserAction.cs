using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;


namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetBossLaser", story: "turn [Value] with [AttackController]", category: "Action", id: "16982cfb618693aeb68001e91c2518ea")]
    public partial class SetBossLaserAction : Action
    {
        [SerializeReference] public BlackboardVariable<bool> Value;
        [SerializeReference] public BlackboardVariable<BurnOutBossAttackController> AttackController;

        protected override Status OnStart()
        {
            AttackController.Value.SetLaserActive(Value.Value);
            return Status.Success;
        }
    }


}
