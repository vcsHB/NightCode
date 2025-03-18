using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Basement.NPC
{
    public class Employee : NPC
    {
        public EmployeeSO employeeInfo;
        public TalkBubble talkBubble;

        //ÀÏ´Ü µð¹ö±×¿ëÀÎµ¥
        public Sprite emotionSprite;

        private Cafe _cafe;
        private Table _table;

        protected override void Awake()
        {
            base.Awake();
            stateMachine.AddState("Move", "Move", employeeInfo.MoveParam);
            stateMachine.AddState("Serving", "Serving", employeeInfo.ServingParam);
            stateMachine.AddState("Service", "Service", employeeInfo.ServiceParam);
            stateMachine.ChangeState("Move");
        }

        public void Init(Cafe cafe, Table targetTable)
        {
            _cafe = cafe;
            _table = targetTable;

            gameObject.SetActive(true);

            SetMoveTarget(_table.servingPositionTrm);
            stateMachine.ChangeState("Move");
            NextState = "Serving";
        }

        public void ServeMenu()
        {
            FoodSO food = _table.customer.RequireFood;
            _table.customer.GetMenu();
            // ¹¹ ¹¹µç ÇØÁàºÁ
        }

        public void DoService()
        {
            talkBubble.SetIcon(emotionSprite);
            talkBubble.Open();
        }

        public void ReturnToCounter()
        {
            talkBubble.Close();
            MoveTarget = _cafe.employeePosition;
            onCompleteMove += OnExit;
            stateMachine.ChangeState("Move");
        }

        private void OnExit()
        {
            _cafe.ReturnEmployee(this);
            onCompleteMove -= OnExit;
        }
    }
}
