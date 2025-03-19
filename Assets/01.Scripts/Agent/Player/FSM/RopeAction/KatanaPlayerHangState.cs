using Agents.Animate;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerHangState : PlayerHangState
    {
        private KatanaPlayerAnimationTrigger _katanaAnimTrigger;
        private float _currentAttackTime = 0f;
        private float _swingAttackDuration = 1.5f;
        private bool _isSwingAttack;
        public KatanaPlayerHangState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _katanaAnimTrigger = player.GetCompo<KatanaPlayerAnimationTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _currentAttackTime = 0f;
            _isSwingAttack = false;
        }


        public override void UpdateState()
        {
            if (_currentAttackTime > 0)
            {
                _currentAttackTime -= Time.deltaTime;
                _katanaAnimTrigger.CastSwingCaster();

            }
            else
            {
                if (_isSwingAttack)
                {
                    _isSwingAttack = false;
                    _player.HealthCompo.SetResist(false);
                }

            }
            base.UpdateState();

        }
        protected override void HandleUseTurbo()
        {
            if (!_player.IsActive) return;
            if (!_canUseTurbo) return;
            _currentAttackTime = _swingAttackDuration;

            ForceUseTurbo();

            _player.HealthCompo.SetResist(true);
        }
    }
}