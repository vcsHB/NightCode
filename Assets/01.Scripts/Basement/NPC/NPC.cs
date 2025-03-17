using Agents.Animate;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Basement.NPC
{
    public abstract class NPC : MonoBehaviour
    {
        public NPCStateMachine stateMachine;
        public NPCRenderer npcRenderer;
        public NPCSO npcSO;

        public float MoveDir { get; protected set; } = 1;
        public Transform MoveTarget { get; protected set; }
        public string NextState { get; protected set; }

        private Rigidbody2D _rigid;

        [HideInInspector] public int curAnimParam;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            //npcRenderer = GetComponent<NPCRenderer>();
            stateMachine = new NPCStateMachine(this);
        }

        protected virtual void Update()
        {
            stateMachine.currentState.UpdateState();
        }


        public void SetNextState(string state)
            => NextState = state;

        public void SetMoveTarget(Transform target)
            => MoveTarget = target;

        public virtual void Move(float direction)
            => transform.position += Vector3.right * (direction * npcSO.speed * Time.deltaTime);

        public void Flip()
        {
            npcRenderer.Flip();
            MoveDir *= -1;
        }

        //일단 임시로 해둔건데 나중에 벽?을 감지할 방법을 만들자!
        public bool IsWallDetected()
            => false;

        public void StopImmediatly()
        {
            MoveDir = 0;
            _rigid.linearVelocityX = 0;
        }
    }
}
