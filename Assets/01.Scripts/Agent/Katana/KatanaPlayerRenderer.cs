
using Agents.Animate;
using UnityEngine;

namespace Agents.Players
{
    public class KatanaPlayerRenderer : PlayerRenderer
    {

        [field: SerializeField] public AnimParamSO Attack1Param;
        [field: SerializeField] public AnimParamSO Attack2Param;
        [field: SerializeField] public AnimParamSO Attack3Param;
    }
}