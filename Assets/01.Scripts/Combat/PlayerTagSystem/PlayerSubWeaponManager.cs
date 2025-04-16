using Agents.Players;
using UI.InGame.GameUI.Combat.SubWeaponSystem;
using UnityEngine;
namespace Combat.SubWeaponSystem
{

    public class PlayerSubWeaponManager : MonoBehaviour
    {
        [SerializeField] private SubWeaponPanel _subWeaponPanel;


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
    }
}

