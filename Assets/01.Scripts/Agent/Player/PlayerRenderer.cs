using System.Data.Common;
using Agents.Animate;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerRenderer : AgentRenderer
    {
        [field:SerializeField] public AnimParamSO IdleParam;
        [field:SerializeField] public AnimParamSO MoveParam;
        [field:SerializeField] public AnimParamSO FallParam;
        [field:SerializeField] public AnimParamSO JumpParam;
        [field:SerializeField] public AnimParamSO SwingParam;
        [field:SerializeField] public AnimParamSO HangParam;


        [field: SerializeField] public AnimParamSO AttackParam; 
        [field: SerializeField] public AnimParamSO SkillParam;
    }
}