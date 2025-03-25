using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/EntityState")]
    public class CafeEntityStateSO : ScriptableObject
    {
        public string stateName;
        public string className;

        public AnimParamSO animParam;
    }
}
