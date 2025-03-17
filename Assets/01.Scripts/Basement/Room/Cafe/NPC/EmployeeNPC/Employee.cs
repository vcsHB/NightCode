using UnityEngine;

namespace Basement.NPC
{
    public class Employee : NPC
    {
        public EmployeeSO employeeInfo;

        private Table _table;

        protected override void Awake()
        {
            base.Awake();

            stateMachine.AddState("Move", "Move", employeeInfo.MoveParam);
            stateMachine.AddState("Serving", "Serving", employeeInfo.ServingParam);
            stateMachine.AddState("Service", "Service", employeeInfo.ServiceParam);
            stateMachine.ChangeState("Move");
        }

        

        public void Init(Table targetTable)
        {
            gameObject.SetActive(true);

            _table = targetTable;
            SetMoveTarget(_table.servingPositionTrm);
            stateMachine.ChangeState("Move");
            NextState = "Serving";
        }
    }
}
