using System;
using UnityEngine;

namespace Base
{
    public class PlayerMoveTarget : MonoBehaviour
    {
        public event Action onCompleteMove;

        private AvatarPlayer _player;
        [SerializeField] private bool _flipOnMoveComplete;
        [SerializeField] private LookDirection _lookDirection;

        private const string _idle = "Idle";
        private const string _moveToTarget = "MoveToTarget";

        
        public void MovePlayer(AvatarPlayer player)
        {
            _player = player;
            _player.SetMoveTarget(transform);
            _player.onCompleteMove += OnCompleteMove;
            _player.stateMachine.ChangeState(_moveToTarget);
        }

        private void OnCompleteMove()
        {
            onCompleteMove?.Invoke();

            if(_flipOnMoveComplete)
            {
                if(_player.MoveDir < -0.5f && _lookDirection == LookDirection.Left)
                    _player.Flip();

                if (_player.MoveDir > 0.5f && _lookDirection == LookDirection.Right)
                    _player.Flip();
            }

            _player.stateMachine.ChangeState(_idle);
            _player.onCompleteMove -= OnCompleteMove;
        }
    }

    public enum LookDirection
    {
        Left,
        Right
    }
}
