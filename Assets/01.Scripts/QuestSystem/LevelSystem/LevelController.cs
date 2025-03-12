using System;
using Combat.PlayerTagSystem;
using UnityEngine;
namespace QuestSystem.LevelSystem
{

    public class LevelController : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private LevelDataSO _levelData;
        private LevelMap _currentLevel;



        private void LoadLevelData()
        {
            // 저장된 데이터에서 불러와서 맵 적용
            //Initialize();
        }

        public void Initialize(LevelDataSO levelData)
        {
            if (_currentLevel != null)
                _currentLevel.Destroy();
            print("맵 생성하기");
            _currentLevel = Instantiate(levelData.mapPrefab, transform.position, Quaternion.identity);
            _playerManager.SetCurrentPlayerPosition(_currentLevel.StartPos);

        }

        public void AddQuestHandler(Action<QuestTargetData> completeHandle)
        {
            _currentLevel.AddQuestHandler(completeHandle);
        }




    }
}