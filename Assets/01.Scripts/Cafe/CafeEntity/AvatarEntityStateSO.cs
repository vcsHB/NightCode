using Agents.Animate;
using UnityEngine;

namespace Base
{
    [CreateAssetMenu(menuName = "SO/Base/EntityState")]
    public class AvatarEntityStateSO : ScriptableObject
    {
        public string stateName;
        public string className;

        public AnimParamSO animParam;
    }
}
