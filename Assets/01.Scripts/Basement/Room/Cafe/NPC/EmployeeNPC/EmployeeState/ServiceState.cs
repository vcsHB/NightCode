//using Agents.Animate;
//using UnityEngine;

//namespace Basement.NPC
//{
//    public class ServiceState : NPCState
//    {
//        private Employee _employee;
//        private float _stateEnterTime;

//        public ServiceState(NPC npc, AnimParamSO animParamSO) : base(npc, animParamSO)
//        {
//            _employee = npc as Employee;
//        }

//        public override void EnterState()
//        {
//            base.EnterState();

//            _stateEnterTime = Time.time;
//            _employee.DoService();
//        }

//        public override void UpdateState()
//        {
//            base.UpdateState();

//            //1초 임기다리는 거는 임시
//            if (_stateEnterTime + 1f < Time.time)
//            {
//                _employee.ReturnToCounter();
//            }
//        }
//    }
//}
