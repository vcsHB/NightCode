using Chipset;
using UnityEngine;
namespace Combat.PlayerTagSystem
{

    public class PlayerChipsetManager : MonoBehaviour, IPlayerSubManager
    {
        [SerializeField] private ChipsetGruopSO _chipsetGroupData;
        private PlayerManager _playerManager;
        public void Initialize(PlayerManager playerManager)
        {
            _playerManager = playerManager;

        }
        

        
        
    }
}