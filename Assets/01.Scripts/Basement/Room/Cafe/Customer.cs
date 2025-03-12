using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement
{
    public class Customer : MonoBehaviour
    {
        public CustomerSO customerInfo;
        public Animator animator;

        private Cafe _cafe;
        private Table _targetTable;
        private Transform _destination;
        private bool _isIdle = false;
        private bool _isSitDown = false;
        private float _sitDownTime;

        private void Update()
        {
            if (_destination == null) return;

            float distance = Vector2.Distance(transform.position, _destination.position);
            float direction = _destination.position.x - transform.position.x;

            if (distance < 0.1f) OnArrive();
            else transform.position += Vector3.right * (direction * customerInfo.customerMoveSpeed * Time.deltaTime);

            if (_isSitDown)
            {
                //앉은 상태에서 뭐 하는거 추가
                if (_sitDownTime + customerInfo.GetRandomStayTime() > Time.time)
                {
                    //이제 나가
                }
            }
        }

        private void OnArrive()
        {
            if (_targetTable == null)
            {
                if (_isIdle)
                {
                    float distance = Vector2.Distance(transform.position, _destination.position);

                    if (distance > 0.1f)
                    {
                        animator.SetBool(customerInfo.idleAnim.hashValue, false);
                        animator.SetBool(customerInfo.moveAnim.hashValue, true);
                    }
                    return;
                }

                _isIdle = true;
                animator.SetBool(customerInfo.moveAnim.hashValue, false);
                animator.SetBool(customerInfo.idleAnim.hashValue, true);
            }
            else
            {
                _isSitDown = true;
                _targetTable.CustomerSitdown(this);
                animator.SetBool(customerInfo.moveAnim.hashValue, false);
                animator.SetBool(customerInfo.sitdownAnim.hashValue, true);
            }
        }

        public void SetDestination(Transform destination)
        {
            _destination = destination;
            animator.SetBool(customerInfo.idleAnim.hashValue, false);
            animator.SetBool(customerInfo.moveAnim.hashValue, true);
        }

        public void SetTable(Table table)
        {
            _targetTable = table;
            SetDestination(table.customerPositionTrm);
        }

        public void OnCompleteSitdown()
        {
            animator.SetBool(customerInfo.sitdownAnim.hashValue, false);
            animator.SetBool(customerInfo.sitStateAnim.hashValue, true);
        }

        public void Init(Cafe cafe)
        {
            _cafe = cafe;
        }
    }
}
