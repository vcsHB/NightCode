using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Basement
{
    public class Customer : MonoBehaviour
    {
        public CustomerSO customerInfo;
        public Animator animator;
        public TalkBubble talkBubble;

        private Cafe _cafe;
        private Vector2 _destination;
        private FoodSO _requireFood;
        private CustomerState _currentState = CustomerState.Enter;
        private float _eatTime;
        private float _stayTime;

        public Table TargetTable { get; private set; }
        public bool IsServedMenu { get; private set; } = false;
        public Vector2 Destination => _destination;

        private void Update()
        {
            if (_destination != null)
            {
                float distance = Vector2.Distance(transform.position, _destination);
                float direction = Mathf.Sign(_destination.x - transform.position.x);

                if (distance < 0.3f) OnArrive();
                else transform.position += Vector3.right * (direction * customerInfo.customerMoveSpeed * Time.deltaTime);
            }

            if (_currentState == CustomerState.Sit)
            {
                if (IsServedMenu == false) return;

                //TODO: 앉은 상태에서 뭐 하는거 추가

                if (_eatTime + _stayTime < Time.time)
                {
                    GoOut();
                }
            }
        }

        private void OnArrive()
        {
            switch (_currentState)
            {
                case CustomerState.Enter:
                    {
                        GoToLine();
                        break;
                    }
                case CustomerState.FindTable:
                    {
                        SetTable();
                        break;
                    }
                case CustomerState.GoingOut:
                    {
                        //나중에 어떻게든 수정해야함
                        Destroy(gameObject);
                        break;
                    }
            }
        }

        private void GoOut()
        {
            animator.SetBool(customerInfo.sitdownAnim.hashValue, false);
            animator.SetBool(customerInfo.standUpAnim.hashValue, true);

            if (customerInfo.CheckGiveTip())
            {
                int money = _requireFood.cost / 3;
                Vector2 position = TargetTable.transform.position + Vector3.up;
                UIManager.Instance.SetPopupText($"<color=green>tip {money}$", position);
            }

            //애니메이션 추가하고 끝날을 때 실행하는걸로
            OnCompleteStandUp();
        }

        private void GoToLine()
        {
            _currentState = CustomerState.Lineup;
            animator.SetBool(customerInfo.moveAnim.hashValue, false);
            animator.SetBool(customerInfo.idleAnim.hashValue, true);
            _cafe.LineUpCustomer(this);

            _requireFood = customerInfo.GetRandomFood();
            talkBubble.Open();
            talkBubble.SetIcon(_requireFood.icon);
        }

        private void SetTable()
        {
            _stayTime = customerInfo.GetRandomStayTime();
            TargetTable.CustomerSitdown(this);

            animator.SetBool(customerInfo.moveAnim.hashValue, false);
            animator.SetBool(customerInfo.sitdownAnim.hashValue, true);

            //애니메이션 추가하고 끝날을 때 실행하는걸로
            OnCompleteSitdown();
        }

        public void ServeMenu()
        {
            IsServedMenu = true;
            _eatTime = Time.time;
            animator.SetBool(customerInfo.sitStateAnim.hashValue, false);
            animator.SetBool(customerInfo.eatAnim.hashValue, true);
        }

        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
            animator.SetBool(customerInfo.idleAnim.hashValue, false);
            animator.SetBool(customerInfo.moveAnim.hashValue, true);
        }

        public void SetTable(Table table)
        {
            _cafe.GetMoney(_requireFood.cost);

            talkBubble.Close();
            TargetTable = table;
            SetDestination(table.customerPositionTrm.position);
            _currentState = CustomerState.FindTable;
        }

        public void OnCompleteSitdown()
        {
            _currentState = CustomerState.Sit;
            animator.SetBool(customerInfo.sitdownAnim.hashValue, false);
            animator.SetBool(customerInfo.sitStateAnim.hashValue, true);
        }

        public void OnCompleteStandUp()
        {
            //이제 나가
            _currentState = CustomerState.GoingOut;
            SetDestination(_cafe.exit.position);
        }

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
            cafe.currentLineTrm.position += Vector3.right * customerInfo.width;
        }

    }
    public enum CustomerState
    {
        Enter,
        Lineup,
        FindTable,
        Sit,
        GoingOut
    }
}
