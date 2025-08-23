using Agents.Players.WeaponSystem;
using Chipset;
using Combat.PlayerTagSystem;
using Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core.DataControl
{
    public class DataLoader : MonoSingleton<DataLoader>
    {
        public UnityEvent onLoad;
        public CharacterDataInitializeSO characterInitialize;
        public ChipsetInitializeSO chipsetInitialize;

        public MapGraphSO mapGraph;
        public ChipsetGroupSO chipsetGroup;
        public PlayerWeaponListSO weaponList;

        [SerializeField] private TipPanel _tutorialPanel;

        private static string _mapSavePath = Path.Combine(Application.dataPath, "Save/MapSave.json");
        private static string _characterSavePath = Path.Combine(Application.dataPath, "Save/CharacterData.json");
        private static string _userDataSavePath = Path.Combine(Application.dataPath, "Save/UserData.json");
        private JsonLoadHelper<ChipsetSave> _chipsetLoadHelper =
            new JsonLoadHelper<ChipsetSave>(Path.Combine(Application.dataPath, "Save/Chipset.json"));

        private MapSave _mapSave;
        private CharacterSave _characterSave;
        private ChipsetSave _chipsetSave;
        private UserData _userData;

        public int Credit => _characterSave.credit;

        public void GoToMenu()
        {
            SceneManager.LoadScene(SceneName.TitleScene);
        }

        protected override void Awake()
        {
            base.Awake();
            Load();

            if (_tutorialPanel != null && _characterSave.characterData.Find(character => character.characterPosition == Vector2.zero) != null)
            {
                _tutorialPanel.Open();
            }
        }

        public void AddCredit(int amount)
        {
            _characterSave.credit += amount;
            Save();
        }

        public MapNodeSO GetCurrentMap()
        {
            if (_mapSave == null) Load();
            return mapGraph.GetNodeSO(_mapSave.enterStageId);
        }

        public List<CharacterEnum> GetCharacters()
        {
            List<CharacterEnum> characters = new List<CharacterEnum>();

            foreach (CharacterEnum character in Enum.GetValues(typeof(CharacterEnum)))
            {
                if (_characterSave.characterData[(int)character].characterPosition == _mapSave.enterStagePosition)
                {
                    characters.Add(character);
                }
            }
            return characters;
        }

        public PlayerWeaponSO GetWeapon(CharacterEnum character)
        {
            if (_characterSave.characterData[(int)character].isPlayerDead) return null;
            return weaponList.GetWeapon(_characterSave.characterData[(int)character].equipWeaponId);
        }

        public UserData GetUserData()
        {
            if (_userData == null) Load();
            return _userData;
        }

        public ChipsetSO[] GetChipset(CharacterEnum character)
        {
            _chipsetSave = _chipsetLoadHelper.Load();
            return _chipsetSave.inventorySave[(int)character].chipsetList.ConvertAll(data => 
            {
                return chipsetGroup.GetChipset(_chipsetSave.containChipset[data.chipsetIndex]);
            }).ToArray();
        }

        public void CompleteMap()
        {
            if (mapGraph.GetNodeSO(_mapSave.enterStageId).nodeType == NodeType.Combat)
            {
                ChipsetSO chipset = RandomUtility.GetRandomInList(chipsetGroup.stageClearReward);

                //보상 설정을 해주는 곳이었는데...
                //_characterSave?.rewardChipsets?.Clear();
                //if (chipset != null)
                //    _characterSave.rewardChipsets.Add(chipset.id);
            }

            _characterSave.clearEnteredStage = true;
            _characterSave.failEnteredStage = false;
            Save();

            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }

        public void FailMap()
        {
            _characterSave.clearEnteredStage = false;
            _characterSave.failEnteredStage = true;
            Save();

            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneName.MapSelectScene);
        }

        public void Load()
        {
            onLoad?.Invoke();

            _chipsetSave = _chipsetLoadHelper.Load();

            if (File.Exists(_mapSavePath) == false)
            {
                _mapSave = new MapSave();

                string json = JsonUtility.ToJson(_mapSave);
                File.WriteAllText(_mapSavePath, json);
            }
            else
            {
                string mapJson = File.ReadAllText(_mapSavePath);
                _mapSave = JsonUtility.FromJson<MapSave>(mapJson);
            }

            if (File.Exists(_characterSavePath) == false)
            {
                _characterSave = new CharacterSave();

                _characterSave.characterData[0].equipWeaponId = characterInitialize.anInitializeWeapon.id;
                _characterSave.characterData[1].equipWeaponId = characterInitialize.jinInitializeWeapon.id;
                _characterSave.characterData[2].equipWeaponId = characterInitialize.binaInitializeWeapon.id;

                string json = JsonUtility.ToJson(_characterSave);
                File.WriteAllText(_characterSavePath, json);
            }
            else
            {
                string characterJson = File.ReadAllText(_characterSavePath);
                _characterSave = JsonUtility.FromJson<CharacterSave>(characterJson);
            }

            if (File.Exists(_userDataSavePath) == false)
            {
                _userData = new UserData();

                string json = JsonUtility.ToJson(_userData);
                File.WriteAllText(_userDataSavePath, json);
            }
            else
            {
                string userDataJson = File.ReadAllText(_userDataSavePath);
                _userData = JsonUtility.FromJson<UserData>(userDataJson);
            }
        }

        //얜 Save용 아님 다 따로 Save 만들어서 관리해줘야함
        public void Save()
        {
            var playerList = PlayerManager.Instance.playerList;
            for (int i = 0; i < playerList.Count; i++)
            {
                _characterSave.characterData[playerList[i].ID].playerHealth
                    = (int)playerList[i].HealthCompo.CurrentHealth;
            }

            string mapJson = JsonUtility.ToJson(_mapSave);
            string characterJson = JsonUtility.ToJson(_characterSave);
            string userJson = JsonUtility.ToJson(_userData);

            File.WriteAllText(_mapSavePath, mapJson);
            File.WriteAllText(_characterSavePath, characterJson);
            File.WriteAllText(_userDataSavePath, userJson);
        }

        public void AddChipset(ushort id)
        {

            Save();
        }

        public void AddWeapon(int id)
        {
            _characterSave.containWeaponList.Add(id);
            Save();
        }

        public bool IsWeaponExist(int id)
            => _characterSave.containWeaponList.Contains(id);

        public float GetHealth(int id)
        {
            return _characterSave.characterData[id].playerHealth;
        }

        public void ResetData()
        {
            File.Delete(_mapSavePath);
            File.Delete(_characterSavePath);
        }
    }
}
