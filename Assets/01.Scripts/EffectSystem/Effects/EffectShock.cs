using System.Collections;
using Agents;
using Combat;
using UnityEngine;

namespace EffectSystem
{
    public class EffectShock : EffectState
    {
        private readonly int _maxChainLinkAmount = 3;
        private readonly float _targetDetectRange = 20f;
        private Collider[] _targets;
        private LayerMask _enemyLayer;
        private bool _canChain;
        private bool _isChained;

        public override void Initialize(Agent agent, bool isResist)
        {
            base.Initialize(agent, isResist);

            _targets = new Collider[10];
            _enemyLayer = LayerMask.GetMask("Enemy");
        }


        public override void Apply(int stack = 1, int level = 1, float percent = 1f)
        {
            base.Apply(level, stack, percent);
            if (currentEffectStack >= stackBurstConditionLevel)
            {
                Over();
                _ownerHealth.ApplyDamage(new CombatData
                {
                    type = AttackType.Effect,
                    damage = level
                });
                // TODO
                // Apply StunStack
            }
        }

        public override void UpdateBySecond()
        {
            EffectType = EffectStateTypeEnum.Shock;
        }

        // private void ChainShock()
        // {
        //     int damage = (int)(level * 1.5f);
        //     _ownerHealth.ApplyDamage(new CombatData()
        //     {
        //         damage = damage,
        //         type = AttackType.Effect
        //     });
        //     _owner.StartCoroutine(ChainShockCoroutine());
        // }

        // private IEnumerator ChainShockCoroutine()
        // {
        //     yield return new WaitForSeconds(0.3f);
        //     int amount = Physics.OverlapSphereNonAlloc(_ownerTrm.position, _targetDetectRange, _targets, _enemyLayer);
        //     int chained = 0;
        //     for (int i = 0; i < amount; i++)
        //     {
        //         if (chained >= _maxChainLinkAmount) // 맥스 체이닝에 도달했으면 취소
        //             break;

        //         if (_targets[i].TryGetComponent(out AgentEffectController effectController))
        //         {
        //             EffectShock shock = effectController.GetEffectState(EffectStateTypeEnum.Shock) as EffectShock;
        //             // effectController에서 GetEffectState해서 shock를 가져오고
        //             // chain가능 여부, shock enable 여부를 따져보고 나서 아래를 실행
        //             if (!shock._canChain)
        //                 continue;

        //             if (shock.isEffectEnabled)
        //             {
        //                 chained++;
        //                 // EnergySphereLaser laser = _owner.gameObject.Pop(EffectPoolType.EnergySphereLaserEffect, _ownerTrm.position + Vector3.up * 3, Quaternion.identity) as EnergySphereLaser;
        //                 // laser.Init(_targets[i].transform, 1);
        //                 // effectController.ApplyEffect(EffectStateTypeEnum.Shock, 1f, 1);
        //                 // yield return new WaitForSeconds(0.2f);
        //                 // laser.Push();

        //             }
        //         }

        //     }
        // }

        public override void SetEffectType()
        {
            EffectType = EffectStateTypeEnum.Shock;
        }
    }
}