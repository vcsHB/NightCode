using Agents.Animate;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerRenderer : AgentRenderer
    {
        [field: SerializeField] public AnimParamSO IdleParam { get; private set; }
        [field: SerializeField] public AnimParamSO MoveParam { get; private set; }
        [field: SerializeField] public AnimParamSO FallParam { get; private set; }
        [field: SerializeField] public AnimParamSO JumpParam { get; private set; }
        [field: SerializeField] public AnimParamSO SwingParam { get; private set; }
        [field: SerializeField] public AnimParamSO HangParam { get; private set; }


        [field: SerializeField] public AnimParamSO AttackParam { get; private set; }
        [field: SerializeField] public AnimParamSO SkillParam { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }
    }
}