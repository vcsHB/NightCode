using Agents.Enemies.BossManage;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Agents.Players;
using Combat;

namespace Agents.Enemies.BossManage.BT.ActionNodes
{

    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "BossSteamCooling", story: "[Boss] start cooling for [Duration] and Apply [Damage]", category: "Action", id: "e18fe26a03c181247b96262718e652a7")]
    public partial class BossSteamCoolingAction : Action
    {
        [SerializeReference] public BlackboardVariable<BurnOutBoss> Boss;
        [SerializeReference] public BlackboardVariable<float> Duration;
        [SerializeReference] public BlackboardVariable<float> Damage;
        private float _startTime;

        protected override Status OnStart()
        {
            _startTime = Time.time;
            Boss.Value.HealthCompo.ApplyDamage(
                new CombatData()
                { type = AttackType.Heat, damage = Damage.Value });

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_startTime + Duration.Value > Time.time)
            {

                return Status.Running;
            }
            return Status.Success;
        }

    }


}