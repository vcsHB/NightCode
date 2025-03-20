using System;
using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Cafe
{
    public class CafeCustomer : CafeEntity
    {
        private CafeTable _table;
        private float _stayingTime;
        private float _foodGetTime;
        private bool _getFood = false;

        protected override void Update()
        {
            base.Update();
            if(_getFood && _foodGetTime + 2f < Time.time)
            {
                MoveTarget = transform.parent;
                
                OnLeaveTable();
                _table.LeaveCustomer();
            }
        }

        //음식을 요청했을 때 => 앉았을 때
        public void RequireFood()
        {
            float direction = Mathf.Sign(_table.transform.position.x - transform.position.x);
            if (LookDir != direction) Flip();

            _getFood = false;
            _table.CustomerSit();
            onCompleteMove -= RequireFood;
            stateMachine.ChangeState("Sit"); 
        }

        //음식을 받았을 때 => 먹기 시작?
        public void OnGetFood()
        {
            _getFood = true;
            _foodGetTime = Time.time;
        }

        public void OnLeaveTable()
        {
            onCompleteMove += OnExitCafe;
            stateMachine.ChangeState("Move");
        }


        public void OnExitCafe()
        {
            Destroy(gameObject);
            onCompleteMove -= OnExitCafe;
        }


        public void Init(CafeTable table)
        {
            _table = table;
            SetMoveTarget(table.customerPosition);
            table.SetCustomer(this);
            onCompleteMove += RequireFood;
        }
    }
}
