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

        private Rigidbody2D _rigid;
        private float _moveDir;

        [HideInInspector] public int curAnimParam;

        protected virtual void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            npcRenderer = GetComponent<NPCRenderer>();
            stateMachine = new NPCStateMachine(this);
        }

        protected virtual void Update()
        {
            stateMachine.currentState.UpdateState();
            Move(_moveDir);
        }

        protected virtual void Move(float direction)
            => _rigid.linearVelocityX = direction * npcSO.speed;

        public void Flip()
        {
            npcRenderer.Flip();
            _moveDir *= -1;
        }

        //일단 임시로 해둔건데 나중에 벽?을 감지할 방법을 만들자!
        public bool IsWallDetected()
            => false;

        public void SetDirection(float direction)
            => _moveDir = direction;

        public void StopImmediatly()
        {
            _moveDir = 0;
            _rigid.linearVelocityX = 0;
        }
    }
}
