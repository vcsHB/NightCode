using System;
using System.Collections.Generic;
using System.Linq;
using Agents.Players;
using Agents.Players.ChipsetSystem;
using Chipset;
using Core.DataControl;
using UnityEngine;
namespace Combat.PlayerTagSystem
{
    public class EnvironmentData
    {
        public Action OnCharacterAmountChangedEvent;
        public int charatcerAmount;
        public int currentAliveCharacterAmount;

    }
    public class PlayerChipsetManager : MonoBehaviour, IPlayerSubManager
    {
        [SerializeField] private ChipsetGroupSO _chipsetGroupData;
        private PlayerManager _playerManager;
        private List<PlayerChipsetController> _playerChipsetControllers = new();
        private List<Player> PlayerList => _playerManager.playerList;
        private EnvironmentData _environmentData;
        private int _currentLeftPlayerAmount;

        public void AfterInit()
        {
            // ChipsetController List Init
            foreach (var player in PlayerList)
            {
                PlayerChipsetController controller = player.GetCompo<PlayerChipsetController>();
                _playerChipsetControllers.Add(controller);
                controller.Initialize(_environmentData);
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

        private void HandlePlayerRetire()
        {
            _environmentData.currentAliveCharacterAmount--;
            _environmentData.OnCharacterAmountChangedEvent?.Invoke();
        }

        public void Initialize(PlayerManager playerManager)
        {
            _playerManager = playerManager;

            _currentLeftPlayerAmount = _playerManager.PlayerDatas.Count;
            _playerManager.OnPlayerDieEvent.AddListener(HandlePlayerRetire);
            _environmentData = new EnvironmentData()
            {
                charatcerAmount = _currentLeftPlayerAmount,
                currentAliveCharacterAmount = _currentLeftPlayerAmount
            };

        }
    }
}