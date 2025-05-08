using Agents.Animate;
using Base.Cafe;
using UnityEngine;

namespace Base
{
    public class PlayerMoveToTargetState : BaseEntityState
    {
        private BasePlayer _player;
        public PlayerMoveToTargetState(BaseEntity npc, AnimParamSO animParamSO) : base(npc, animParamSO)
        {
            _player = npc as BasePlayer;
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
