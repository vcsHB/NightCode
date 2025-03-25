using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agents;
using Agents.Players;
using HUDSystem;
using InputManage;
using ObjectManage.Rope;
using UI.InGame.GameUI.CharacterSelector;
using UI.InGame.SystemUI.AlertSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.PlayerTagSystem
{

    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        public UnityEvent OnAllPlayerDieEvent;
        [Header("Essential Settings")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private List<PlayerSO> _playerDatas;
        [SerializeField] private List<Player> _playerList;
        [SerializeField] private AimGroupController _aimGroup;
        [SerializeField] private AlertGroup _alertGroup;
        [SerializeField] private int _currentPlayerIndex = 0;
        [SerializeField] private CharacterSelectWindow _characterSelectWindow;
        public Player CurrentPlayer => _playerList[_currentPlayerIndex];
        public Transform CurrentPlayerTrm => CurrentPlayer.transform;
        public PlayerSO CurrentPlayerData => _playerDatas[_currentPlayerIndex];
        public bool IsAllRetire => _playerList.All(x => x.IsDead);

        [Header("Change Setting")]
        [SerializeField] private float _changeCooltime = 1f;
        private float _currentCooltime = 0f;

        private void Start()
        {
            Initialize();
            _playerInput.OnCharacterChangeEvent += Change;
            SetPlayer(CurrentPlayer);
        }

        private void Update()
        {
            _currentCooltime += Time.deltaTime;

        }

        private void OnDestroy()
        {
            _playerInput.OnCharacterChangeEvent -= Change;

        }

        private void Initialize()
        {
            for (int i = 0; i < _playerDatas.Count; i++)
            {
                Player playerCharacter = Instantiate(_playerDatas[i].playerPrefab, transform);
                playerCharacter.GetComponentInChildren<AimController>().SetAimGroup(_aimGroup);
                _playerList.Add(playerCharacter);
                _characterSelectWindow.AddCharacterSlot(_playerDatas[i], playerCharacter);
                playerCharacter.SetActive(false);
                playerCharacter.SetStartDisable(i != 0);
                playerCharacter.OnDieEvent += HandlePlayerDie;
            }

            CameraControllers.CameraManager.Instance.SetFollow(CurrentPlayer.transform);
            _aimGroup.SetAnchorOwner(CurrentPlayer.RigidCompo, CurrentPlayer.RopeHolder);
            SetPlayer(CurrentPlayer);
            _characterSelectWindow.SelectCharacter(CurrentPlayerData.id);

        }

        /// <summary>
        /// Change Character Func
        /// </summary>
        public void Change()
        {
            if (_currentPlayerIndex < 0) return;
            if (_currentCooltime < _changeCooltime) return;
            _currentCooltime = 0f;

            if (CurrentPlayer.CanCharacterChange)
                StartCoroutine(ChangeCoroutine());
        }

        private IEnumerator ChangeCoroutine()
        {
            Transform prevPlayerTrm = CurrentPlayer.transform;
            Vector2 changePosition = prevPlayerTrm.position;

            Player prevPlayer = CurrentPlayer;

            int prevIndex = _currentPlayerIndex;
            for (int i = 0; i < _playerList.Count; i++)
            {

                _currentPlayerIndex = (_currentPlayerIndex + 1) % _playerList.Count; // index change -> character Change
                if (!CurrentPlayer.IsDead)
                    break;
            }
            if (prevIndex == _currentPlayerIndex)
            {

                if (IsAllRetire)
                {
                    OnAllPlayerDieEvent?.Invoke();
                    Debug.Log("All Player die");
                }
                yield break;
            }

            float direction = prevPlayer.GetCompo<AgentRenderer>().FacingDirection;
            prevPlayer.ExitCharacter();
            prevPlayer.SetActive(false);

            yield return new WaitForSeconds(0.2f);
            _characterSelectWindow.SelectCharacter(CurrentPlayerData.id);
            CurrentPlayer.transform.position = changePosition;
            //CurrentPlayer.transform.rotation = prevRotation;
            CurrentPlayer.GetCompo<AgentRenderer>().FlipController(direction);
            SetPlayer(CurrentPlayer);
        }

        private void SetPlayer(Player newCharacter)
        {
            newCharacter.EnterCharacter();
            newCharacter.SetActive(true);
            HUDController.Instance.SetFollowTarget(newCharacter.transform);
            CameraControllers.CameraManager.Instance.SetFollow(newCharacter.transform);
            _aimGroup.SetAnchorOwner(newCharacter.RigidCompo, newCharacter.RopeHolder);
        }

        private void HandlePlayerDie()
        {
            Change();
        }

        public void SetCurrentPlayerPosition(Vector2 position)
        {
            CurrentPlayer.transform.position = position;
        }


        public void StopPlayer()
        {
            AimController aimController = CurrentPlayer.GetCompo<AimController>();
            aimController.RemoveWire();
            PlayerMovement movement = CurrentPlayer.GetCompo<PlayerMovement>();
            movement.StopImmediately(false);
            movement.SetVelocity(Vector2.zero);
            movement.SetMovement(0f);

        }

        public void AddPlayer(PlayerSO playerSO)
        {
            if (_playerDatas.Contains(playerSO))
            {
                Debug.LogWarning($"Add duplicated character. name:{playerSO.characterName}");
                return;
            }
            _alertGroup.ShowAlert($"AGENT [{playerSO.characterName}]님이 합류하였습니다.");
            Player playerCharacter = Instantiate(playerSO.playerPrefab, transform);
            playerCharacter.GetComponentInChildren<AimController>().SetAimGroup(_aimGroup);
            _playerDatas.Add(playerSO);
            _playerList.Add(playerCharacter);
            _characterSelectWindow.AddCharacterSlot(playerSO, playerCharacter);
            playerCharacter.ExitCharacter();
            playerCharacter.SetActive(false);
            playerCharacter.SetStartDisable(false);
            playerCharacter.OnDieEvent += HandlePlayerDie;
        }

    }

}