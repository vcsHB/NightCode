//using Agents.Animate;
//using UnityEngine;

//namespace Basement.NPC
//{
//    public class EatState : NPCState
//    {
//        private float _eatStartTime;
//        private Customer _customer;

//        public EatState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
//        {
//            _customer = npc as Customer;
//        }

//        public override void EnterState()
//        {
//            base.EnterState();
//            _eatStartTime = Time.time;
//        }

//        public override void UpdateState()
//        {
//            base.UpdateState();

//            if(_eatStartTime + _customer.FoodEatingTime < Time.time)
//            {
//                _customer.PayCost();
//            }
//        }
//    }
//}
