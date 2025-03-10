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

            _currentLevel = Instantiate(levelData.mapPrefab);
            _playerManager.SetCurrentPlayerPosition(_currentLevel.StartPos);

        }




    }
}