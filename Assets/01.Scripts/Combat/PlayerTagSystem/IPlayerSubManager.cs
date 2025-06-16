using UnityEngine;
namespace Combat.PlayerTagSystem
{

    public interface IPlayerSubManager
    {
        public void Initialize(PlayerManager playerManager);
        public void AfterInit();
    }
}