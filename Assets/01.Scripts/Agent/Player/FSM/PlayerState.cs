using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;
        protected PlayerMovement _mover;


        public PlayerState(Player player, PlayerStateMachine stateMachine, int animationHash)
        {
            _player = player;
            _stateMachine= stateMachine;
            _mover = player.GetCompo<PlayerMovement>();
        }


        public virtual void Enter()
        {

        }
        public virtual void UpdateState()
        {
            
        }

        public virtual void Exit()
        {

        }
    }
}