using Chipset;
using Core;
using Core.DataControl;
using System.Collections.Generic;
using System.IO;
using UI.GameSelectScene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Map
{
    public class CharacterDataController : MonoBehaviour
    {
        private JsonLoadHelper<CharacterSave> _loadHelper =
            new JsonLoadHelper<CharacterSave>(Path.Combine(Application.dataPath, "Save/CharacterData.json"));

        [SerializeField] private CharacterDataInitializeSO _initializeData;

        [Space]
        [SerializeField] private RewardPanel _rewardPanel;
        [SerializeField] private MapController _mapController;
        [SerializeField] private CharacterBuildController _characterBuildController;

        //이걸 넘기고 다른 스크립트에서 이걸 가지고 뭘 하게 하는 느낌일거임
        private CharacterSave _characterSave;

        private void Awake()
        {
            Load();
            _mapController.OnEnterStage += Save;
            _mapController.Initialize(_characterSave.characterData.ConvertAll(data => data.characterPosition));

            CheckClearFailCurrentNode();

            _mapController.InitCharacterController();
            _characterBuildController.Initialize(_characterSave);

            _characterSave.clearEnteredStage = false;
            _characterSave.failEnteredStage = false;
            _mapController.SetCompleteNode();
        }

        public void Save()
        {
            for (int i = 0; i < _characterSave.characterData.Count; i++)
            {
                _characterSave.characterData[i].equipWeaponId =
                    _characterBuildController.GetCharacterWeapon((CharacterEnum)i);
                _characterSave.characterData[i].chipsetInventoryData =
                    _characterBuildController.GetCharacterInventory((CharacterEnum)i);
                _characterSave.characterData[i].characterPosition =
                    _mapController.GetCharacterPosition((CharacterEnum)i);
            }

            _loadHelper.Save(_characterSave);
        }

        public void Load()
        {
            _characterSave = _loadHelper.Load();

            if (_characterSave.rewardChipsets.Count > 0)
            {
                _rewardPanel.Open();

                List<ChipsetSO> rewards = new();
                for (int i = 0; i < _characterSave.rewardChipsets.Count; i++)
                {
                    ushort id = _characterSave.rewardChipsets[i];
                    ChipsetSO chipset = DataLoader.Instance.chipsetGroup.GetChipset(id);
                    rewards.Add(chipset);

                    _characterSave.containChipsetList.Add(id);
                }

                _rewardPanel.SetReward(rewards);
                _characterSave.rewardChipsets.Clear();
            }

            if (_characterSave.containWeaponList.Count <= 0) InitializeData();
        }

        public void LoadTitleScene()
        {
            SceneManager.LoadScene(SceneName.TitleScene);
        }

        private void InitializeData()
        {
            _characterSave.openInventory = _initializeData.openInventory;
            _initializeData.containChipsets.ForEach(chipset => _characterSave.containChipsetList.Add(chipset.id));

            _characterSave.characterData[(int)CharacterEnum.An].equipWeaponId = _initializeData.anInitializeWeapon.id;
            _characterSave.characterData[(int)CharacterEnum.JinLay].equipWeaponId = _initializeData.jinInitializeWeapon.id;
            _characterSave.characterData[(int)CharacterEnum.Bina].equipWeaponId = _initializeData.binaInitializeWeapon.id;

            for (int i = 0; i < 3; i++)
            {
                _characterSave.characterData[i].playerHealth = _initializeData.characterDefaultHealth;
                _characterSave.containWeaponList.Add(_characterSave.characterData[i].equipWeaponId);
            }
        }

        private void CheckClearFailCurrentNode()
        {
            if (_characterSave.clearEnteredStage)
            {
                for (int i = 0; i < _characterSave.characterData.Count; ++i)
                {
                    if (_characterSave.characterData[i].playerHealth <= 0)
                    {
                        _characterSave.characterData[i].isPlayerDead = true;
                        _mapController.RetireCharacter((CharacterEnum)i);
                    }
                }
                _mapController.ClearCurrentStage();
            }
            if (_characterSave.failEnteredStage)
            {
                int remainCharacter = _characterSave.characterData.Count;
                for (int i = 0; i < _characterSave.characterData.Count; ++i)
                {
                    if (_characterSave.characterData[i].characterPosition == _mapController.CurrentEnterPosition)
                    {
                        _characterSave.characterData[i].isPlayerDead = true;
                        _mapController.RetireCharacter((CharacterEnum)i);
                    }

                    if (_characterSave.characterData[i].isPlayerDead) remainCharacter--;
                }

                if (remainCharacter <= 0)
                {
                    _mapController.ResetData();
                    _loadHelper.ResetData();
                    SceneManager.LoadScene(SceneName.TitleScene);
                }
            }
        }
    }
}
