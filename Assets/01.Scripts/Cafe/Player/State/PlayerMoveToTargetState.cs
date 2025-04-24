using Agents.Animate;
using UnityEngine;

namespace Cafe
{
    public class PlayerMoveToTargetState : CafeEntityState
    {
        private CafePlayer _player;
        public PlayerMoveToTargetState(CafeEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as CafePlayer;
        }

        public override void UpdateState()
        {
            float distance = Mathf.Abs(_player.MoveTarget.position.x - _player.transform.position.x);

            if (distance < 0.1f)
            {
                _player.onCompleteMove?.Invoke();
                return;
            }

            float direction = Mathf.Sign(_player.MoveTarget.position.x - _player.transform.position.x);
            _player.Move(direction);
        }
    }
}
