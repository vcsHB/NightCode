using Agents;
using Agents.Players;
using Combat.Casters;
namespace Combat
{

    public class PlayerKnockbackBody : KnockbackBody, IAgentComponent
    {
        private Player _player;
        private PlayerMovement _playerMovement;
        protected override void Awake()
        {
            base.Awake();
        }
        public override void ApplyKnockback(KnockbackCasterData knockbackData)
        {
            _playerMovement.StopImmediately(true);
            _playerMovement.SetVelocity(knockbackData.powerDirection);
            _player.StateMachine.ChangeState("Swing");
            //base.ApplyKnockback(knockbackData);
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _playerMovement = _player.GetCompo<PlayerMovement>();
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }
    }
}