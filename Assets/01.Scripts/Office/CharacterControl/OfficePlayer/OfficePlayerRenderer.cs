using Agents;
using Agents.Animate;
using UnityEngine;
namespace Office.CharacterControl
{

    public class OfficePlayerRenderer : AgentRenderer
    {
        [field: SerializeField] public AnimParamSO IdleParam { get; private set; }
        [field: SerializeField] public AnimParamSO MoveParam { get; private set; }
        [field: SerializeField] public AnimParamSO InteractParam { get; private set; }

    }
}