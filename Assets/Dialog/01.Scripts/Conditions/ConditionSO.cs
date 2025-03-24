using UnityEngine;

namespace Dialog
{
    public abstract class ConditionSO : ScriptableObject
    {
        public abstract bool Decision();
    }
}
