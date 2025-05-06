using Base.Cafe;
using Base.Entity;
using System;
using UnityEngine;

namespace Base
{
    public abstract class BaseEntity : MonoBehaviour
    {
        public BaseEntityStateMachine stateMachine;
        public EntityRenderer npcRenderer;
        public BaseEntitySO npcSO;

        public Action onCompleteMove;
        public float LookDir { get; protected set; } = 1;
        public Transform MoveTarget { get; protected set; }
        public float MoveDir => npcRenderer.MoveDirection;


        private Rigidbody2D _rigid;
        [HideInInspector] public int curAnimParam;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>(); ;
            stateMachine = new BaseEntityStateMachine(this);

            npcSO.entityStateList.ForEach(state => stateMachine.AddState(state.stateName, state.className, state.animParam));
            stateMachine.ChangeState(npcSO.entityStateList[0].stateName);
        }

        protected virtual void Update()
        {
            stateMachine.currentState.UpdateState();
        }

        public virtual void SetMoveTarget(Transform target)
            => MoveTarget = target;

        public virtual void Move(float dir)
        {
            if (Mathf.Sign(dir) != Mathf.Sign(LookDir))
                Flip();

            _rigid.linearVelocityX = dir * npcSO.moveSpeed;
        }

        public void StopImmediatly()
        {
            _rigid.linearVelocityX = 0;
        }

        public void Flip()
        {
            npcRenderer.Flip();
            LookDir *= -1;
        }
    }
}
