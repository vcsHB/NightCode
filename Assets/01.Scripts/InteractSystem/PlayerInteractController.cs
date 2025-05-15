using Agents;
using Agents.Players;
namespace InteractSystem
{

    public class PlayerInteractController : InteractController
    {
        protected Player _ownerPlayer;
        public override void Initialize(Agent agent)
        {
            base.Initialize(agent);
            _ownerPlayer = agent as Player;
            _ownerPlayer.PlayerInput.OnInteractEvent += HandleInteract;

        }

        private void OnDestroy()
        {
            _ownerPlayer.PlayerInput.OnInteractEvent -= HandleInteract;

        }

        private void HandleInteract()
        {
            if (!_ownerPlayer.IsActive) return;
            TryInteract();
        }
    }
}