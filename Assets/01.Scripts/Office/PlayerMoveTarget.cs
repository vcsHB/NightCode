using System;
using UnityEngine;

namespace Base
{
    public class PlayerMoveTarget : MonoBehaviour
    {
        public event Action onCompleteMove;

        private BasePlayer _player;
        [SerializeField] private bool _flipOnMoveComplete;
        [SerializeField] private bool _lookRightOnMoveComplete;

        private const string _idle = "Idle";
        private const string _moveToTarget = "MoveToTarget";

        
        public void MovePlayer(BasePlayer player)
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
                if(_player.MoveDir > 0.5f && _lookRightOnMoveComplete == false)
                    _player.Flip();

                if (_player.MoveDir < -0.5f && _lookRightOnMoveComplete)
                    _player.Flip();
            }

            _player.stateMachine.ChangeState(_idle);
            _player.onCompleteMove -= OnCompleteMove;
        }
    }
}
