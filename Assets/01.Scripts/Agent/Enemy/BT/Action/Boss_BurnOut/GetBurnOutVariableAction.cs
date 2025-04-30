using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GetBurnOutVariable", story: "get burnOut boss variable from [Boss]", category: "Action", id: "05e8f80c06f2d8f2ce2a00d584997f68")]
    public partial class GetBurnOutVariableAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutBoss> Boss;

        protected override Status OnStart()
        {
            Enemy enemy = Boss.Value;

            enemy.SetVariable("Mover", enemy.GetCompo<BurnOutBossMovement>());
            enemy.SetVariable("Renderer", enemy.GetCompo<AgentRenderer>());
            enemy.SetVariable("AnimTrigger", enemy.GetCompo<EnemyAnimationTrigger>());
            enemy.SetVariable("Collider", enemy.GetComponent<Collider2D>());
            enemy.SetVariable("PhaseController", enemy.GetCompo<BossPhaseController>());
            enemy.SetVariable("HeatController", enemy.GetComponent<BossHeatController>());


            return Status.Running;
        }

    }


}