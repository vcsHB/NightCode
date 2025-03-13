using Agents.Animate;
using System.Collections.Generic;
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
        public AnimParamSO standUpAnim;

        public float width;
        public Vector2 stayTimeRange;
        public List<FoodSO> canOrderFoods;
        [Header("devide to 10000")]
        public float tipChance;

        public bool CheckGiveTip()
        {
            float random = Random.Range(0, 10000);
            return tipChance <= random;
        }

        public float GetRandomStayTime()
            => Random.Range(stayTimeRange.x, stayTimeRange.y);

        public FoodSO GetRandomFood()
            => canOrderFoods[Random.Range(0, canOrderFoods.Count)];
    }
}
