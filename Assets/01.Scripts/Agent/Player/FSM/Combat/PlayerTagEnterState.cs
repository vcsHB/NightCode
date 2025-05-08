using Agents.Animate;
using FeedbackSystem;
using ObjectManage.VFX;
using StatSystem;
using Unity.Burst.Intrinsics;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerTagEnterState : PlayerState
    {
        private AgentStatus _status;
        private StatSO _speedStat;
        private FeedbackCreateEventData _createFeedbackData = new FeedbackCreateEventData("Avoid");
        private FeedbackFinishEventData _finishFeedbackData = new FeedbackFinishEventData("Avoid");
        public PlayerTagEnterState(Player player, PlayerStateMachine stateMachine, AnimParamSO animParam) : base(player, stateMachine, animParam)
        {
            _canUseRope = false;
            _status = _player.GetCompo<AgentStatus>();
            _speedStat = _status.GetStat(StatusEnumType.Speed);
        }

        public override void Enter()
        {
            if (_player.HealthCompo != null)
                _player.HealthCompo.SetResist(true);
            _player.CanCharacterChange = false;
            _player.gameObject.SetActive(true);
            _mover.SetGravityMultiplier(0f);
            _player.EventChannel.RaiseEvent(_createFeedbackData);

            //Vector2 dashDirection = new Vector2(_renderer.FacingDirection, 0);
            Vector2 playerPosition = _player.transform.position;
            Vector2 dashDirection = (Vector2)Camera.main.ScreenToWorldPoint(_player.PlayerInput.MousePosition) - playerPosition;
            dashDirection.Normalize();
            float dashPower = _speedStat.Value * 10f;
            DashVFXPlayer vfx = PoolManager.Instance.Pop(ObjectPooling.PoolingType.DashVFX) as DashVFXPlayer;
            vfx.Play(playerPosition, dashDirection, 7f);
            
            _mover.SetVelocity(dashDirection * dashPower);

            base.Enter();
            _renderer.SetDissolve(true);
        }

        public override void Exit()
        {
            base.Exit();
            _player.CanCharacterChange = true;
            _player.EventChannel.RaiseEvent(_finishFeedbackData);
            _mover.ResetGravityMultiplier();
            if (_player.HealthCompo != null)
                _player.HealthCompo.SetResist(false);


        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            _stateMachine.ChangeState("Idle");
        }
    }
}