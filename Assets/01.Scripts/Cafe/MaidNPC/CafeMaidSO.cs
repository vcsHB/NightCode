using UnityEngine;

namespace Base.Cafe
{
    [CreateAssetMenu(menuName = "SO/Cafe/EmployeeSO")]
    public class CafeMaidSO : ScriptableObject
    {
        public CafeMaid maidPf;
        public float moveSpeed;
    }
}
