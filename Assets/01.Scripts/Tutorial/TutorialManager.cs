using Combat.PlayerTagSystem;
using QuestSystem.LevelSystem;
using UnityEngine;

namespace Tutorial
{

    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private LevelMap _tutorialLevel;

        
        private void Start()
        {
            MovePlayerToStartPos();
        }

        private void MovePlayerToStartPos()
        {
            _playerManager.SetCurrentPlayerPosition(_tutorialLevel.StartPos);

        }


    }

}