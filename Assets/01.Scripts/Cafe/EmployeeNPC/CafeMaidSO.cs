using UnityEngine;

namespace Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/EmployeeSO")]
    public class CafeMaidSO : ScriptableObject
    {
        public CafeMaid maidPf;
        public float moveSpeed;
    }
}
