using Agents.Players;
using Combat.SubWeaponSystem;
using UI.InGame.GameUI.Combat.SubWeaponSystem;
using UnityEngine;
namespace Combat.PlayerTagSystem
{

    public class PlayerSubWeaponManager : MonoBehaviour, IPlayerSubManager
    {
        [SerializeField] private SubWeaponPanel _subWeaponPanel;
        private PlayerManager _playerManager;

        public void Initialize(PlayerManager playerManager)
        {
            _playerManager = playerManager;
            _playerManager.OnPlayerChangedEvent += SetSubWeapon;
        }

        private void OnDestroy()
        {
            _playerManager.OnPlayerChangedEvent -= SetSubWeapon;
        }

        public void SetSubWeapon(Player prevPlayer, Player newPlayer)
        {
            if (prevPlayer != null)
            {
                SubWeaponController prevController = prevPlayer.GetCompo<SubWeaponController>();
                prevController.OnWeaponCountChangedEvent
                    -= _subWeaponPanel.GetDetailPanel(prevController.SubWeaponSO.type).HandleWeaponCountChange;
            }
            SubWeaponController currentController = newPlayer.GetCompo<SubWeaponController>();
            WeaponDetailPanel detailPanel = _subWeaponPanel.GetDetailPanel(currentController.SubWeaponSO.type);
            currentController.OnWeaponCountChangedEvent
                += detailPanel.HandleWeaponCountChange;
            detailPanel.SetData(currentController.SubWeaponSO);
            _subWeaponPanel.SetWeapon(currentController.SubWeaponSO);

        }

        public void AfterInit()
        {
        }
    }
}

