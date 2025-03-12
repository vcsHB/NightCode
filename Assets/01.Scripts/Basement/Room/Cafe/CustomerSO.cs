using Agents.Animate;
using UnityEngine;

namespace Basement
{
    [CreateAssetMenu(menuName = "SO/Basement/Cafe/CustomerSO")]
    public class CustomerSO : ScriptableObject
    {
        public string customerName;
        public Customer customerPf;
        public float customerMoveSpeed;

        public AnimParamSO idleAnim;
        public AnimParamSO moveAnim;
        public AnimParamSO sitdownAnim;
        public AnimParamSO sitStateAnim;
        public AnimParamSO eatAnim;

        public float width;
        public Vector2 stayTimeRange;

        public float GetRandomStayTime()
            => Random.Range(stayTimeRange.x, stayTimeRange.y);
    }
}
