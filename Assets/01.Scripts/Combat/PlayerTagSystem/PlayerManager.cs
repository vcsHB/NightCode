using Agents;
using Agents.Enemies;
using Agents.Players;
using Core.DataControl;
using HUDSystem;
using InputManage;
using ObjectManage.Rope;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.InGame.GameUI.CharacterSelector;
using UI.InGame.SystemUI.AlertSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Combat.PlayerTagSystem
{

    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        // Events
        public UnityEvent OnAllPlayerDieEvent;
        public UnityEvent OnPlayerDieEvent;

        /// <summary>
        /// param : <Previous Player, Current Player>
        /// </summary>
        public event Action<Player, Player> OnPlayerChangedEvent;
        [Header("Essential Settings")]
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerGroupDataSO _playerGroupData;
        [SerializeField] private List<PlayerSO> _playerDatas;
        public List<PlayerSO> PlayerDatas => _playerDatas;
        [SerializeField] internal List<Player> playerList;
        [SerializeField] private AimGroupController _aimGroup;
        [SerializeField] private AlertGroup _alertGroup;
        [SerializeField] private int _currentPlayerIndex = 0;
        [SerializeField] private CharacterSelectWindow _characterSelectWindow;
        [SerializeField] private PlayerSubWeaponManager _playerSubWeaponManager;

        [Header("Loader Settings")]
        [SerializeField] private MapLoader _mapLoader;
        [SerializeField] private EnemyIndicatorController _indicator;

        public Player CurrentPlayer => playerList[_currentPlayerIndex];
        public Transform CurrentPlayerTrm => CurrentPlayer.transform;
        public PlayerSO CurrentPlayerData => _playerDatas[_currentPlayerIndex];
        public bool IsAllRetire => playerList.All(x => x.IsDead);
        public bool useDebugMode;

        private Dictionary<Type, IPlayerSubManager> _subManagers = new Dictionary<Type, IPlayerSubManager>();


        [Header("Change Setting")]
        [SerializeField] private float _changeCooltime = 1f;
        private float _currentCooltime = 0f;

        protected override void Awake()
        {
            base.Awake();
            GetComponentsInChildren<IPlayerSubManager>(true)
              .ToList().ForEach(controller => _subManagers.Add(controller.GetType(), controller));

            foreach (IPlayerSubManager subManager in _subManagers.Values)
            {
                subManager.Initialize(this);
            }
            OnAllPlayerDieEvent.AddListener(PlayerAllDieEvent);
        }

        private void OnDestroy()
        {
            _playerInput.ResetAllSubscription();

        }

        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_subManagers.TryGetValue(typeof(T), out IPlayerSubManager compo))
            {
                return compo as T;
            }

            if (!isDerived) return default;

            Type findType = _subManagers.Keys.FirstOrDefault(x => x.IsSubclassOf(typeof(T)));
            if (findType != null)
                return _subManagers[findType] as T;

            return default(T);
        }
        private void Start()
        {
            Initialize();
            _playerInput.OnCharacterChangeEvent += Change;
            //OnAllPlayerDieEvent.AddListener(StageManager.Instance.LoadCurrentScene);
            //OnAllPlayerDieEvent.AddListener(StageManager.Instance.ReloadCurrentScene);
        }

        private void Update()
        {
            _currentCooltime += Time.deltaTime;

        }

        private void PlayerAllDieEvent()
        {
            _playerInput.OnCharacterChangeEvent -= Change;
            _playerInput.ResetAllSubscription();
            Time.timeScale = 0.1f;
        }

        private void Initialize()
        {
            // GetData;
            if (!useDebugMode)
            {
                var characterInfo = DataLoader.Instance.GetCharacters();

                for (int i = 0; i < characterInfo.Count; i++)
                {
                    _playerDatas.Add(_playerGroupData.GetPlayerData((int)characterInfo[i]));
                }
            }


            for (int i = 0; i < _playerDatas.Count; i++)
            {
                Player playerCharacter = Instantiate(_playerDatas[i].playerPrefab, transform);
                playerCharacter.GetComponentInChildren<AimController>().SetAimGroup(_aimGroup);
                playerList.Add(playerCharacter);
                _characterSelectWindow.AddCharacterSlot(_playerDatas[i], playerCharacter);
                playerCharacter.SetActive(false);
                playerCharacter.SetStartDisable(i != 0);
                playerCharacter.OnDieEvent += HandlePlayerDie;
            }
            Invoke(nameof(AfterInit), 2f);
            _aimGroup.SetAimColor(CurrentPlayerData.personalColor);
            CameraControllers.CameraManager.Instance.SetFollow(CurrentPlayer.transform);
            _aimGroup.SetAnchorOwner(CurrentPlayer.RigidCompo, CurrentPlayer.RopeHolder);
            SetPlayer(CurrentPlayer);
            _characterSelectWindow.SelectCharacter(CurrentPlayerData.id);

            if (!_mapLoader.useDebugMode)
            {

                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].transform.position = _mapLoader.CurrentLevel.StartPos;
                    //if (!useDebugMode)
                }
            }
        }
        private void AfterInit()
        {
            foreach (var subManager in _subManagers)
            {
                subManager.Value.AfterInit();
            }

            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].HealthCompo.SetHealthData(DataLoader.Instance.GetHealth(playerList[i].ID));
            }
            CurrentPlayer.HealthCompo.UpdateHealthData();
        }

        public void Change(int index)
        {
            Change(index, false);
        }
        /// <summary>
        /// Change Character Func
        /// </summary>
        public void Change(int index, bool isForce = false)
        {
            if (!isForce && _currentCooltime < _changeCooltime) return;
            if (index == _currentPlayerIndex || index < 0 || index >= playerList.Count) return;
            if (!playerList[index].IsDead && CurrentPlayer.CanCharacterChange)
            {
                _currentCooltime = 0f;
                StartCoroutine(ChangeCoroutine(index));
            }
        }

        public void ChangeNextPlayer(bool isForce = false)
        {
            if (!isForce && _currentCooltime < _changeCooltime) return;
            if (_currentPlayerIndex < 0) return;

            int nextIndex = FindNextAvailablePlayerIndex();
            if (nextIndex == _currentPlayerIndex)
            {
                if (IsAllRetire)
                {
                    OnAllPlayerDieEvent?.Invoke();
                    Debug.Log("All Player die");
                }
                return;
            }

            if (CurrentPlayer.CanCharacterChange)
            {
                _currentCooltime = 0f;
                StartCoroutine(ChangeCoroutine(nextIndex));
            }
        }

        /// <summary>
        /// 실제 플레이어 전환 처리
        /// </summary>
        private IEnumerator ChangeCoroutine(int targetIndex)
        {
            Transform prevPlayerTrm = CurrentPlayer.transform;
            Vector2 changePosition = prevPlayerTrm.position;
            float direction = CurrentPlayer.GetCompo<AgentRenderer>().FacingDirection;

            Player prevPlayer = CurrentPlayer;

            prevPlayer.ExitCharacter();
            prevPlayer.SetActive(false);

            _currentPlayerIndex = targetIndex;

            yield return new WaitForSeconds(0.2f);

            OnPlayerChangedEvent?.Invoke(prevPlayer, CurrentPlayer);
            _characterSelectWindow.SelectCharacter(CurrentPlayerData.id);
            CurrentPlayer.transform.position = changePosition;
            CurrentPlayer.GetCompo<AgentRenderer>().FlipController(direction);

            SetPlayer(CurrentPlayer);
            _aimGroup.SetAimColor(CurrentPlayerData.personalColor);
        }

        /// <summary>
        /// 현재 플레이어 인덱스를 기준으로 다음 사용 가능한 플레이어 인덱스를 반환
        /// </summary>
        private int FindNextAvailablePlayerIndex()
        {
            int startIndex = _currentPlayerIndex;
            for (int i = 1; i < playerList.Count; i++)
            {
                int index = (startIndex + i) % playerList.Count;
                if (!playerList[index].IsDead)
                    return index;
            }
            return _currentPlayerIndex;
        }

        private void SetPlayer(Player newCharacter)
        {
            newCharacter.EnterCharacter();
            newCharacter.SetActive(true);
            HUDController.Instance.SetFollowTarget(newCharacter.transform);
            CameraControllers.CameraManager.Instance.SetFollow(newCharacter.transform);
            _aimGroup.SetAnchorOwner(newCharacter.RigidCompo, newCharacter.RopeHolder);
            _indicator.transform.parent = newCharacter.transform;
        }

        private void HandlePlayerDie()
        {
            ChangeNextPlayer(true);
            OnPlayerDieEvent?.Invoke();
        }

        public void SpawnEnemy(Enemy enemy)
            => _indicator.AddEnemy(enemy);

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
            playerList.Add(playerCharacter);
            _characterSelectWindow.AddCharacterSlot(playerSO, playerCharacter);
            playerCharacter.ExitCharacter();
            playerCharacter.SetActive(false);
            playerCharacter.SetStartDisable(false);
            playerCharacter.OnDieEvent += HandlePlayerDie;
        }
    }

}