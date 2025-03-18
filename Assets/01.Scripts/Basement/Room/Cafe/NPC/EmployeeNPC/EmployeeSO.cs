using Agents.Animate;
using UnityEngine;

namespace Basement.NPC
{
    [CreateAssetMenu(fileName = "EmployeeSO", menuName = "SO/Basement/Cafe/EmployeeSO")]
    public class EmployeeSO : ScriptableObject
    {
        public Employee employeePf;

        public AnimParamSO MoveParam;
        public AnimParamSO ServingParam;
        public AnimParamSO ServiceParam;
        public AnimParamSO ReturnParam;
    }
}

