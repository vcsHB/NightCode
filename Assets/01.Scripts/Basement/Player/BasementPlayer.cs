using Agents;
using InputManage;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = InputManage.PlayerInput;

namespace Basement.Player
{
    public class BasementPlayer : Agent
    {
        [field: SerializeField] public PlayerInput playerInput { get; private set; }
        public event Action onInteract;

        private float _xVelocity;
        private bool _wallDetected;
        private bool _readyInteract;

        private void FixedUpdate()
        {
            Move();
            Interact();
        }

        private void Move()
        {
            float preveDir = _xVelocity;
            _xVelocity = playerInput.InputDirection.x;


        }

        private void Interact()
        {
            //나중에 뉴인풋에서 키 추가해서 바꿔줘야함
            //Update에서 실행시키지 말고 구독으로 관리하게 바꿔주
            if (Keyboard.current.fKey.wasPressedThisFrame)
                onInteract?.Invoke();
        }
    }
}
