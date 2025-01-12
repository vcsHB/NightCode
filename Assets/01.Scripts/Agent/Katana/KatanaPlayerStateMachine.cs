    using UnityEngine;
namespace Agents.Players.FSM
{

    public class KatanaPlayerStateMachine : PlayerStateMachine
    {
        KatanaPlayerRenderer _katanaRenderer;
        public KatanaPlayerStateMachine(Player player) : base(player)
        {
            _katanaRenderer = _player.GetCompo<KatanaPlayerRenderer>();
        }

        public override void Initialize(string firstState)
        {
            AddState("Attack1", "KatanaPlayerAttack1", _katanaRenderer.Attack1Param);
            //AddState("Attack2", "KatanaPlayerAttack2", _katanaRenderer.Attack2Param);
            //AddState("Attack3", "KatanaPlayerAttack3", _katanaRenderer.Attack3Param);


            base.Initialize(firstState);
        }
    }
}
