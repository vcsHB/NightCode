using Map;
using QuestSystem.LevelSystem;
using UnityEngine;

namespace Core.DataControl
{
    public class MapLoader : MonoBehaviour
    {
        private MapNodeSO _currentMap;
        private LevelMap levelInstance;
        public bool useDebugMode;
        public LevelMap CurrentLevel => levelInstance;

        private void Awake()
        {
            if (useDebugMode) return;
            _currentMap = DataLoader.Instance.GetCurrentMap();

            if (_currentMap == null)
            {
                Debug.LogError("Current map is null. Please check the MapLoader setup.");
                return;
            }

            levelInstance = Instantiate(_currentMap.levelPrefab);
        }
    }
}
