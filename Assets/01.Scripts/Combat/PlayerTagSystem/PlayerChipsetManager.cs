using System.Collections.Generic;
using Agents.Players;
using Agents.Players.ChipsetSystem;
using Chipset;
using Core.DataControl;
using UnityEngine;
namespace Combat.PlayerTagSystem
{

    public class PlayerChipsetManager : MonoBehaviour, IPlayerSubManager
    {
        [SerializeField] private ChipsetGruopSO _chipsetGroupData;
        private PlayerManager _playerManager;
        private List<PlayerChipsetController> _playerChipsetControllers = new();
        private List<Player> PlayerList => _playerManager.playerList;

        public void AfterInit()
        {
            // ChipsetController List Init
            foreach (var player in PlayerList)
            {
                _playerChipsetControllers.Add(player.GetCompo<PlayerChipsetController>());
            }

            // 각 플레이어 데이터에 대해 칩셋 로드 및 적용
            for (int i = 0; i < _playerManager.PlayerDatas.Count; i++)
            {
                var chipsets = DataLoader.Instance.GetChipset((CharacterEnum)_playerManager.PlayerDatas[i].id);

                foreach (var chipset in chipsets)
                {
                    if (chipset.chipsetType == ChipsetType.Personal)
                    {
                        _playerChipsetControllers[i].AddChipsetFunction(chipset);
                    }
                    else // Global
                    {
                        foreach (var controller in _playerChipsetControllers)
                        {
                            controller.AddChipsetFunction(chipset);
                        }
                    }
                }
            }
        }


        public void Initialize(PlayerManager playerManager)
        {
            _playerManager = playerManager;

        }




    }
}