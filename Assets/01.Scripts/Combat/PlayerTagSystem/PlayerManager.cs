using System.Collections.Generic;
using Agents.Players;
using CameraControllers;
using InputManage;
using ObjectManage.Rope;
using UnityEngine;

namespace Combat.PlayerTagSystem
{

    public class PlayerManager : MonoBehaviour
    {
        [Header("Essential Settings")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerSO[] _playerDatas;
        [SerializeField] private List<Player> _playerList;
        [SerializeField] private AimGroupController _aimGroup;
        [SerializeField] private int _currentPlayerIndex;
        public Player CurrentPlayer => _playerList[_currentPlayerIndex];

        private void Awake()
        {
            Initialize();
            CurrentPlayer.EnterCharacter();
            _playerInput.OnCharacterChangeEvent += Change;
        }
        private void Start()
        {

        }

        private void OnDestroy()
        {
            _playerInput.OnCharacterChangeEvent -= Change;

        }

        private void Initialize()
        {
            for (int i = 0; i < _playerDatas.Length; i++)
            {
                Player playerCharacter = Instantiate(_playerDatas[i].playerPrefab, transform);
                playerCharacter.GetComponentInChildren<AimController>().SetAimGroup(_aimGroup);
                _playerList.Add(playerCharacter);
            }
            CameraManager.Instance.SetFollow(CurrentPlayer.transform);
            _aimGroup.SetAnchorOwner(CurrentPlayer.RigidCompo, CurrentPlayer.RopeHolder);
        }


        public void Change()
        {
            CurrentPlayer.ExitCharacter();
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _playerList.Count;
            CurrentPlayer.EnterCharacter();
            CameraManager.Instance.SetFollow(CurrentPlayer.transform);
            _aimGroup.SetAnchorOwner(CurrentPlayer.RigidCompo, CurrentPlayer.RopeHolder);
        }


    }

}