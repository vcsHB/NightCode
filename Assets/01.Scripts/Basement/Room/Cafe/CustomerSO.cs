//using Agents.Animate;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Basement.NPC
//{
//    [CreateAssetMenu(menuName = "SO/Basement/Cafe/CustomerSO")]
//    public class CustomerSO : ScriptableObject
//    {
//        public string customerName;
//        public Customer customerPf;
//        public float customerMoveSpeed;

//        public AnimParamSO MoveParam;
//        public AnimParamSO SitdownParam;
//        public AnimParamSO SitParam;
//        public AnimParamSO EatParam;
//        public AnimParamSO StandupParam;

//        public float width;
//        public Vector2 foodEatingTime;
//        public List<FoodSO> canOrderFoods;
//        [Header("devide to 10000")]
//        public float tipChance;

//        public bool CheckGiveTip()
//        {
//            float random = Random.Range(0, 10000);
//            return tipChance <= random;
//        }

//        public float GetRandomEatingTime()
//            => Random.Range(foodEatingTime.x, foodEatingTime.y);

//        public FoodSO GetRandomFood()
//            => canOrderFoods[Random.Range(0, canOrderFoods.Count)];
//    }
//}
